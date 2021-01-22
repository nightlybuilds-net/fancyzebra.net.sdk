using System;
using System.Runtime.Serialization;

namespace fancyzebra.net.sdk.core.Exceptions
{
    public class ConnectivityException: Exception
    {
        public ConnectivityException()
        {
        }

        protected ConnectivityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConnectivityException(string message) : base(message)
        {
        }

        public ConnectivityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}