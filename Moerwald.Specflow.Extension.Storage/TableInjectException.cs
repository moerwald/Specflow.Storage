using System;
using System.Runtime.Serialization;

namespace Moerwald.Specflow.Storage
{
    public class TableInjectException : Exception
    {
        public TableInjectException()
        {
        }

        public TableInjectException(string message) : base(message)
        {
        }

        public TableInjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TableInjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
