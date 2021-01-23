using System;
using System.Runtime.Serialization;

namespace fancyzebra.net.sdk.core.Exceptions
{
    public class AcceptanceException: Exception
    {
        public AcceptanceException()
        {
        }

        protected AcceptanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public AcceptanceException(string message) : base(message)
        {
        }

        public AcceptanceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}