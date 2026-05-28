namespace ThuleSignal.Domain.Entities
{
    public class PodcastTrack : Track
    {
        public string Podcaster { get; set; }

        public PodcastTrack(string id, string title, int duration, string filePath, string podcaster) 
            : base(id, title, duration, filePath)
        {
            Podcaster = podcaster;
        }

        public override string GetPlaybackSource()
        {
            return $"Подкаст від {Podcaster}. Файл: {FilePath}";
        }
    }
}