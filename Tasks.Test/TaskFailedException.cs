using System;
using System.Runtime.Serialization;

namespace Tasks.Test
{
    [Serializable]
    internal class TaskFailedException : Exception
    {
        public TaskFailedException()
        {
        }

        public TaskFailedException(string message) : base(message)
        {
        }

        public TaskFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaskFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}