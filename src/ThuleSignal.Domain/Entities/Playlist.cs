using System;
using System.Collections.Generic;
using System.Linq;

namespace ThuleSignal.Domain.Entities
{
    public class Playlist
    {
        private readonly List<Track> _tracks = new List<Track>();
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва плейлиста не може бути порожньою.");
                _name = value;
            }
        }

        public IReadOnlyList<Track> Tracks => _tracks.AsReadOnly();

        public int TotalDurationInSeconds => _tracks.Sum(t => t.DurationInSeconds);

        public int Count => _tracks.Count;

        public Playlist(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            Name = name; 
        }

        public Track this[int index]
        {
            get
            {
                if (index < 0 || index >= _tracks.Count)
                    throw new IndexOutOfRangeException("Індекс поза межами плейлиста.");
                return _tracks[index];
            }
        }

        public Track this[string title]
        {
            get
            {
                var track = _tracks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                return track ?? throw new KeyNotFoundException($"Трек з назвою '{title}' не знайдено.");
            }
        }

        public static Playlist operator +(Playlist playlist, Track track)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));
            if (track == null) throw new ArgumentNullException(nameof(track));

            if (playlist._tracks.Any(t => t.Id == track.Id))
            {
                return playlist;
            }

            playlist._tracks.Add(track);
            return playlist;
        }

        public static Playlist operator +(Playlist left, Playlist right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            var combinedPlaylist = new Playlist($"{left.Name} & {right.Name}");
            
            foreach (var track in left._tracks) combinedPlaylist += track;
            foreach (var track in right._tracks) combinedPlaylist += track;

            return combinedPlaylist;
        }

        public static bool operator ==(Playlist? left, Playlist? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            if (left.Name != right.Name || left.Count != right.Count) return false;

            for (int i = 0; i < left.Count; i++)
            {
                if (left._tracks[i].Id != right._tracks[i].Id) return false;
            }

            return true;
        }

        public static bool operator !=(Playlist? left, Playlist? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Playlist playlist && this == playlist;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, _tracks.Count);
        }
    }
}