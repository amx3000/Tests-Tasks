using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public static class TaskExtension
    {
        public static async Task<TResult> ExecuteAsync<TResult>(this ITask<TResult> task, CancellationToken cancellationToken)
        {
            return await Task.Run(() => task.Execute(cancellationToken), cancellationToken);
        }
    }
}
