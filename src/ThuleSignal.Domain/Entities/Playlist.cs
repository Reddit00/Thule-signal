using System;
using System.Collections.Generic;
using ThuleSignal.Domain.Entities;
namespace ThuleSignal.Domain.Entities 
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Track> Tracks { get; }

        public Playlist(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Tracks = new List<Track>();
        }

        public void AddTrack(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));
            Tracks.Add(track);
            Console.WriteLine($"[Domain.Playlist] Трек '{track.Title}' додано до плейлиста '{Name}'.");
        }
    }
}