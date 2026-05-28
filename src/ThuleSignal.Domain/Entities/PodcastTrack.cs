namespace ThuleSignal.Domain.Entities
{
    public class PodcastTrack : Track
    {
        public string Podcaster { get; set; }

        public PodcastTrack(string id, string title, int duration, string filePath, string artistId, string podcaster) 
            : base(id, title, duration, filePath, Genre.Podcast, artistId)
        {
            Podcaster = podcaster;
        }

        public override string GetPlaybackSource() => $"Подкаст від {Podcaster}.";
    }
}