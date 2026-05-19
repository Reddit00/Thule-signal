using System;
using System.Collections.Generic;
using ThuleSignal.Domain.Common;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Patterns.Iterator
{
    public class PlaylistIterator : IPlaylistIterator
    {
        private readonly List<Track> _tracks;
        private int _position = -1;

        public int CurrentIndex => _position;

        public PlaylistIterator(List<Track> tracks)
        {
            _tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));
        }

        public bool HasNext()
        {
            return _position + 1 < _tracks.Count;
        }
        public Track Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("Кінець плейлиста досягнуто.");
            }
            
            _position++;
            return _tracks[_position];
        }
        public void Reset()
        {
            _position = -1;
        }
    }
}