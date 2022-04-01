using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    [Serializable]
    public class UnableReadDatabaseException : Exception
    {
        public UnableReadDatabaseException() { }

        public UnableReadDatabaseException(string? message) : base(message) { }

        public UnableReadDatabaseException(string? message, Exception? innerException) : base(message, innerException) { }

        protected UnableReadDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}