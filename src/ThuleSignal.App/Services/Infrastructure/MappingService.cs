using System.Linq;
using ThuleSignal.App.Dto;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public static class MappingService
    {
        public static PlaylistDto ToDto(Playlist playlist)
        {
            return new PlaylistDto
            {
                Name = playlist.Name,
                Tracks = playlist.Tracks.Select(t => new TrackDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    DurationInSeconds = t.DurationInSeconds,
                    FilePath = t.FilePath,
                    TrackGenre = t.TrackGenre.ToString(),
                    ArtistId = t.ArtistId
                }).ToList()
            };
        }
        public static Playlist ToDomain(PlaylistDto dto)
        {
            var playlist = new Playlist(dto.Name);

            foreach (var trackDto in dto.Tracks)
            {
                string detectedType = trackDto.TrackGenre == "Podcast" ? "PODCAST" : "STREAM";
                
                Track domainTrack = TrackFactory.CreateTrack(
                    detectedType,
                    trackDto.Id,
                    trackDto.Title,
                    trackDto.FilePath,
                    trackDto.ArtistId 
                );

                playlist += domainTrack; 
            }

            return playlist;
        }
    }
}