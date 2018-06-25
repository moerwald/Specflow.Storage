using System;
using System.Runtime.Serialization;

namespace SpecflowExtension.Storage
{
    public class TableParsingException : Exception
    {
        public TableParsingException()
        {
        }

        public TableParsingException(string message) : base(message)
        {
        }

        public TableParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TableParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
