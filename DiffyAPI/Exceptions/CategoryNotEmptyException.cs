using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class CategoryNotEmptyException : Exception
    {
        public CategoryNotEmptyException() { }

        public CategoryNotEmptyException(string? message) : base(message) { }

        public CategoryNotEmptyException(string? message, Exception? innerException) : base(message, innerException) { }

        protected CategoryNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
