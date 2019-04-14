using System;

namespace Tasks.Test
{
    internal class TestFailTask : TestTaskBase, ITask<int>
    {
        public TestFailTask(TimeSpan? executionTimeout = null) : base(executionTimeout)
        {
        }

        protected override int ExecuteCore()
        {
            throw new TaskFailedException("Task failed");
        }
    }
}