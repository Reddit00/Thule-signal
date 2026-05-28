using System;

namespace ThuleSignal.Domain.Exceptions
{
    public class ThuleSignalException : Exception
    {
        public ThuleSignalException(string message) : base(message) { }
        public ThuleSignalException(string message, Exception innerException) : base(message, innerException) { }
    }
} 