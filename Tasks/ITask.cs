using System.Threading;

namespace Tasks
{
    public interface ITask<TResult>
    {
        TResult Execute(CancellationToken cancellationToken);
    }
}
