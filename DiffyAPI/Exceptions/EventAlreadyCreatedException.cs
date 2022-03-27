using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class EventAlreadyCreatedException : Exception
    {
        public EventAlreadyCreatedException() { }

        public EventAlreadyCreatedException(string? message) : base(message) { }

        public EventAlreadyCreatedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected EventAlreadyCreatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
