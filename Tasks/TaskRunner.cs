using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public class TaskRunner
    {
        private static readonly TimeSpan DefaultTaskExecutionTimeout = TimeSpan.FromSeconds(1);

        public TResult RunTasks<TResult>(
            ITaskSelectionStrategy<TResult> taskSelectionStrategy,
            TimeSpan? timeout = null)
        {
            if (taskSelectionStrategy == null)
                throw new ArgumentNullException(nameof(taskSelectionStrategy));

            if (timeout == null)
                timeout = DefaultTaskExecutionTimeout;

            var runningTasks = new List<Task<(bool IsError, TResult Result)>>();
            var cts = new CancellationTokenSource();
            while (true)
            {
                // берем очередную задачу
                var task = taskSelectionStrategy.GetNextTask();
                if (task == null)
                {
                    // все, задачи кончились
                    break;
                }

                // запускаем задачу асинхронно
                var t = ExecuteTaskAsync(task, cts.Token);

                // ждем, что закончится раньше - запущенная задача или таймаут
                if (await Task.WhenAny(t, Task.Delay(timeout.Value, cts.Token)) == t)
                {
                    // задача завершилась до истечения таймаута
                    if (!t.Result.IsError)
                    {
                        // задача завершилась без ошибок, отменяем остальные уже запущенные задачи и возвращаем результат
                        cts.Cancel();
                        return t.Result.Result;
                    }
                }
                else
                {
                    // за отведенное время задача не выполнилась, добавляем ее в список исполняющихся и продолжаем цикл
                    runningTasks.Add(t);
                }
            }

            // если есть запущенные задачи
            while (runningTasks.Count > 0)
            {
                // ждем завершения любой задачи
                var i = Task.WaitAny(runningTasks.ToArray(), cts.Token);
                var t = runningTasks[i];

                if (t.Result.IsError)
                {
                    // задача завершилась с ошибкой, удаляем ее из списка запущеных и продолжаем цикл
                    runningTasks.RemoveAt(i);
                    continue;
                }

                // если задача завершилась без ошибки, отменяем остальные и возвращаем результат
                cts.Cancel();
                return t.Result.Result;
            }

            throw new AllTasksFailedException();
        }

        /// <summary>
        /// Выполняет задачу асихронно.
        /// </summary>
        /// <returns>Возвращает признак, что задача завершилась с ошибкой, и результат выполнения задачи (если не было ошибки)</returns>
        private async Task<(bool IsError, TResult Result)> ExecuteTaskAsync<TResult>(ITask<TResult> task, CancellationToken cancellationToken)
        {
            try
            {
                var result = await task.ExecuteAsync(cancellationToken);
                return (false, result);
            }
            catch (Exception e)
            {
                return (true, default(TResult));
            }
        }
    }
}
