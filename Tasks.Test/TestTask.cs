using System;

namespace Tasks.Test
{
    internal class TestTask : TestTaskBase
    {
        private int returnValue;

        public TestTask(int returnValue, TimeSpan? executionTimeout = null) : base(executionTimeout)
        {
            this.returnValue = returnValue;
        }

        protected override int ExecuteCore()
        {
            return returnValue;
        }
    }
}