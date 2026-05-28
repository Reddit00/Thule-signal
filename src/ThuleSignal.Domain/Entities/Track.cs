using System;
using ThuleSignal.Domain.Common;

namespace ThuleSignal.Domain.Entities
{
    public abstract class Track : IEntity, IDisposable
    {
        private bool _disposed = false;
        public string Id { get; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public virtual int DurationInSeconds { get; set; } 

        protected Track(string id, string title, int durationInSeconds, string filePath)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = title ?? "Unknown Track";
            DurationInSeconds = durationInSeconds > 0 ? durationInSeconds : throw new ArgumentException("Duration must be positive");
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public abstract string GetPlaybackSource();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}