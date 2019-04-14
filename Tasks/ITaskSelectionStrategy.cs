namespace Tasks
{
    public interface ITaskSelectionStrategy<TResult>
    {
        ITask<TResult> GetNextTask();
    }
}
