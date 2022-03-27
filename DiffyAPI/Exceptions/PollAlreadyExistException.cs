using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class PollAlreadyExistException : Exception
    {
        public PollAlreadyExistException() { }

        public PollAlreadyExistException(string? message) : base(message) { }

        public PollAlreadyExistException(string? message, Exception? innerException) : base(message, innerException) { }

        protected PollAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
