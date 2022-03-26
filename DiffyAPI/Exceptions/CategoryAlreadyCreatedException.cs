using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class CategoryAlreadyCreatedException : Exception
    {
        public CategoryAlreadyCreatedException() { }

        public CategoryAlreadyCreatedException(string? message) : base(message) { }

        public CategoryAlreadyCreatedException(string? message, Exception? innerException) : base(message, innerException) { }

        protected CategoryAlreadyCreatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
