using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class UserNotUploadedException : Exception
    {
        public UserNotUploadedException() { }

        public UserNotUploadedException(string? message) : base(message) { }

        public UserNotUploadedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected UserNotUploadedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
