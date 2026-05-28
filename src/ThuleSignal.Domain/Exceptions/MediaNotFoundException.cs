namespace ThuleSignal.Domain.Exceptions
{
    public class MediaNotFoundException : ThuleSignalException
    {
        public string ResourcePath { get; }
        public MediaNotFoundException(string message, string resourcePath) : base(message)
        {
            ResourcePath = resourcePath;
        }
    }
}