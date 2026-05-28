using System.Collections.Generic;

namespace ThuleSignal.App.Dto
{
    public class PlaylistDto
    {
        public string Name { get; set; } = string.Empty;
        public List<TrackDto> Tracks { get; set; } = new();
    }
}