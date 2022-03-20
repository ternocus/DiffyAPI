using System.Runtime.Serialization;

namespace DiffyAPI.Database
{
    [Serializable]
    internal class UnableReadDatabaseException : Exception
    {
        public UnableReadDatabaseException() {}

        public UnableReadDatabaseException(string? message) : base(message) {}

        public UnableReadDatabaseException(string? message, Exception? innerException) : base(message, innerException) {}

        protected UnableReadDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}