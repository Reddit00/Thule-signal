using System;

namespace ThuleSignal.Domain.Entities
{
    public class PodcastTrack : Track
    {
        public string Podcaster { get; set; }
        public string EpisodeName { get; set; }

        public PodcastTrack(string id, string title, int duration, string source, string podcaster, string episodeName)
            : base(id, title, duration, source)
        {
            Podcaster = podcaster;
            EpisodeName = episodeName;
            TrackGenre = Genre.Podcast; 
        }

        public override string GetPlaybackSource()
        {
            return FilePath;
        }
    }
}