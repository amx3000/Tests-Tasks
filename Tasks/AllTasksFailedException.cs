using System;
using System.Runtime.Serialization;

namespace Tasks
{
    [Serializable]
    internal class AllTasksFailedException : Exception
    {
        public AllTasksFailedException()
        {
        }

        public AllTasksFailedException(string message) : base(message)
        {
        }

        public AllTasksFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AllTasksFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}