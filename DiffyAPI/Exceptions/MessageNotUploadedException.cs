using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class MessageNotUploadedException : Exception
    {
        public MessageNotUploadedException() { }

        public MessageNotUploadedException(string? message) : base(message) { }

        public MessageNotUploadedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected MessageNotUploadedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
