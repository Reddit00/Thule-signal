using Microsoft.EntityFrameworkCore;
using ThuleSignal.App.Dto;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class ThuleDbContext : DbContext
    {
        public DbSet<TrackDto> DbTracks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=thule_signal.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackDto>().HasKey(t => t.Id);
        }
    }
}