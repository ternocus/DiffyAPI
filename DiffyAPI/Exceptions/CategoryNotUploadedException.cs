using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class CategoryNotUploadedException : Exception
    {
        public CategoryNotUploadedException() { }

        public CategoryNotUploadedException(string? message) : base(message) { }

        public CategoryNotUploadedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected CategoryNotUploadedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
