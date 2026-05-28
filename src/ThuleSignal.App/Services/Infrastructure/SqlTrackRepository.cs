using System;
using System.Collections.Generic;
using System.Linq;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.App.Dto;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class SqlTrackRepository : IRepository<Track>
    {
        private readonly ThuleDbContext _context;

        public SqlTrackRepository()
        {
            _context = new ThuleDbContext();
            _context.Database.EnsureCreated(); 
        }

        public void Add(Track entity)
        {
            if (entity == null) return;
            
            
            var dto = new TrackDto
            {
                Id = entity.Id,
                Title = entity.Title,
                DurationInSeconds = entity.DurationInSeconds,
                FilePath = entity.FilePath,
                TrackGenre = entity.TrackGenre.ToString(),
                ArtistId = entity.ArtistId
            };

            if (!_context.DbTracks.Any(t => t.Id == dto.Id))
            {
                _context.DbTracks.Add(dto);
                _context.SaveChanges(); 
            }
        }

        public Track? GetById(string id)
        {
            var dto = _context.DbTracks.Find(id);
            if (dto == null) return null;
            
           
            string type = dto.TrackGenre == "Podcast" ? "PODCAST" : "STREAM";
            return TrackFactory.CreateTrack(type, dto.Id, dto.Title, dto.FilePath, dto.ArtistId);
        }

        public IReadOnlyList<Track> GetAll()
        {
            var list = new List<Track>();
            var dtos = _context.DbTracks.ToList();

            foreach (var dto in dtos)
            {
                string type = dto.TrackGenre == "Podcast" ? "PODCAST" : "STREAM";
                list.Add(TrackFactory.CreateTrack(type, dto.Id, dto.Title, dto.FilePath, dto.ArtistId));
            }

            return list.AsReadOnly();
        }

        public void Remove(string id)
        {
            var dto = _context.DbTracks.Find(id);
            if (dto != null)
            {
                _context.DbTracks.Remove(dto);
                _context.SaveChanges(); 
            }
        }
    }
}