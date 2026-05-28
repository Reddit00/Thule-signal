using System;

namespace ThuleSignal.Domain.Exceptions
{
    public class NetworkStreamingException : ThuleSignalException
    {
        public string StreamUrl { get; }

        public NetworkStreamingException(string message, string streamUrl) : base(message)
        {
            StreamUrl = streamUrl;
        }
        public NetworkStreamingException(string message, string streamUrl, Exception inner) : base(message, inner)
        {
            StreamUrl = streamUrl;
        }
    }
}