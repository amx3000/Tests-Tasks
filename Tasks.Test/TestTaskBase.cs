using System;
using System.Threading;

namespace Tasks.Test
{
    internal abstract class TestTaskBase : ITask<int>
    {
        private static readonly TimeSpan DefaultExecutionTimeout = TimeSpan.FromSeconds(0.5);

        private readonly TimeSpan executionTimeout;

        public TestTaskBase(TimeSpan? executionTimeout = null)
        {
            this.executionTimeout = executionTimeout ?? DefaultExecutionTimeout;
        }

        public int Execute()
        {
            Thread.Sleep((int)executionTimeout.TotalMilliseconds);
            return ExecuteCore();
        }

        protected abstract int ExecuteCore();
    }
}