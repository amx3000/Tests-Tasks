using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tasks;

namespace Tasks.Test
{
    [TestClass]
    public class TaskRunnerTests
    {
        TaskRunner TaskRunner { get; } = new TaskRunner();

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TaskRunnerReturnsSingleTasksResult()
        {
            var task = new TestTask(7);
            var strategy = new TestTasksSelectionStrategy(task);

            var result = TaskRunner.RunTasks(strategy);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        [ExpectedException(typeof(TaskFailException))]
        public void TaskRunnerFailedWithSingleFailedTask()
        {
            var task = new TestFailTask();
            var strategy = new TestTasksSelectionStrategy(task);

            TaskRunner.RunTasks(strategy);
        }
    }
}
