﻿using System.Runtime.Serialization;

namespace DiffyAPI.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException() { }

        public MessageNotFoundException(string? message) : base(message) { }

        public MessageNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

        protected MessageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
