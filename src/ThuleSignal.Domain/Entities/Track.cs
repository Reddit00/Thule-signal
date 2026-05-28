using System;
using ThuleSignal.Domain.Common;

namespace ThuleSignal.Domain.Entities
{
    public abstract class Track : MediaComponent, IEntity, IDisposable
    {
        private bool _disposed = false;
        public string Id { get; }
        public string FilePath { get; set; }
        public virtual int DurationInSeconds { get; set; }
        public Genre TrackGenre { get; set; }
        public string ArtistId { get; set; }
        protected Track(string id, string title, int durationInSeconds, string filePath, Genre genre, string artistId) 
            : base(title)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            DurationInSeconds = durationInSeconds > 0 ? durationInSeconds : throw new ArgumentException("Duration must be positive");
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            TrackGenre = genre;
            ArtistId = artistId ?? throw new ArgumentNullException(nameof(artistId));
        }

        protected Track(string id, string title, int durationInSeconds, string filePath) 
            : base(title)
        {
            Id = id;
            DurationInSeconds = durationInSeconds;
            FilePath = filePath;
            ArtistId = "unknown";
        }

        public abstract string GetPlaybackSource();

        public override void DisplayStructure(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} [Трек] {Title} ({DurationInSeconds} сек.)");
        }

        public override int GetTotalDuration() => DurationInSeconds;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed) _disposed = true;
        }
    }
}