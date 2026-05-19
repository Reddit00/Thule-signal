using System;

namespace ThuleSignal.Domain.Entities
{
    public class Track : IDisposable
    {
        private bool _disposed = false;

        public string Id { get; }
        public string Title { get; set; }
        public int DurationInSeconds { get; set; }
        public string FilePath { get; set; }

        public Track(string id, string title, int durationInSeconds, string filePath)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = title ?? "Unknown Track";
            DurationInSeconds = durationInSeconds > 0 ? durationInSeconds : throw new ArgumentException("Duration must be positive");
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }
        public Track(Track other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Id = Guid.NewGuid().ToString();
            Title = other.Title + " (Copy)";
            DurationInSeconds = other.DurationInSeconds;
            FilePath = other.FilePath;
        }

        ~Track() => Dispose(false);

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