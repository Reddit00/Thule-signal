namespace ThuleSignal.App.Dto
{
    public class TrackDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int DurationInSeconds { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string TrackGenre { get; set; } = string.Empty;
        public string ArtistId { get; set; } = string.Empty;
    }
}