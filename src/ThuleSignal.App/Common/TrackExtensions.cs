using System;
using System.Collections.Generic;
using System.Linq;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Common
{
    public static class TrackExtensions
    {
        public static IEnumerable<Track> GetLongTracks(this IEnumerable<Track> tracks)
        {
            return tracks.Where(t => t.DurationInSeconds > 180);
        }
        public static IEnumerable<Track> FilterByGenre(this IEnumerable<Track> tracks, Genre genre)
        {
            return tracks.Where(t => t.TrackGenre == genre);
        }
    }
}