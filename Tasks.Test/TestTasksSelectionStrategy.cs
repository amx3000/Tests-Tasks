using System.Collections.Generic;

namespace Tasks.Test
{
    internal class TestTasksSelectionStrategy : ITaskSelectionStrategy<int>
    {
        private readonly Stack<ITask<int>> tasks;

        public TestTasksSelectionStrategy(params ITask<int>[] tasks)
        {
            this.tasks = new Stack<ITask<int>>(tasks);
        }

        public ITask<int> GetNextTask()
        {
            return tasks.Count > 0 ? tasks.Pop() : null;
        }
    }
}