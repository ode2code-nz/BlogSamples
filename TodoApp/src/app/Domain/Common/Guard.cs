using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Todo.Domain.Common
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void Against(bool precondition, string message = "")
        {
            if (!precondition)
                throw new ContractException(message);
        }
    }

    [Serializable]
    public class ContractException : Exception
    {
        public ContractException()
        {
        }

        public ContractException(string message)
            : base(message)
        {
        }

        public ContractException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
