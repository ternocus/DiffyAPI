using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class MessageAlreadyCreatedException : Exception
    {
        public MessageAlreadyCreatedException() { }

        public MessageAlreadyCreatedException(string? message) : base(message) { }

        public MessageAlreadyCreatedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected MessageAlreadyCreatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
