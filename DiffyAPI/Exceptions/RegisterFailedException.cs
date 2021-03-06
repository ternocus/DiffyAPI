using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() { }

        public RegisterFailedException(string? message) : base(message) { }

        public RegisterFailedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected RegisterFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
