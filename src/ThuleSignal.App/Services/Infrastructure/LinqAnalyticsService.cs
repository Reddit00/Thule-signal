using System;
using System.Collections.Generic;
using System.Linq;
using ThuleSignal.App.Common;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class LinqAnalyticsService
    {
        public void ExecuteAnalytics(List<Track> tracks, List<Artist> artists)
        {
            Console.WriteLine("АНАЛІТИКА МЕДІАТЕКИ ЧЕРЕЗ LINQ");

            Console.WriteLine("\n[1] Фільтрація підкастів довших за 100 сек:");

            var longPodcastsMethod = tracks
                .Where(t => t.TrackGenre == Genre.Podcast && t.DurationInSeconds > 100)
                .Select(t => t.Title);

            var longPodcastsQuery = from t in tracks
                                    where t.TrackGenre == Genre.Podcast && t.DurationInSeconds > 100
                                    select t.Title;

            Console.WriteLine($"-> Знайдено через Method Syntax: {longPodcastsMethod.Count()} шт.");
            Console.WriteLine($"-> Знайдено через Query Syntax: {longPodcastsQuery.Count()} шт.");
            Console.WriteLine("\n[2] Запит JOIN (Треки з інформацією про країну автора):");

            var trackWithArtistInfo = tracks.Join(
                artists,
                track => track.ArtistId,  
                artist => artist.Id,      
                (track, artist) => new    
                {
                    TrackName = track.Title,
                    ArtistName = artist.Name,
                    ArtistCountry = artist.Country
                });

            foreach (var item in trackWithArtistInfo)
            {
                Console.WriteLine($"   Композиція: '{item.TrackName}' | Виконавець: {item.ArtistName} ({item.ArtistCountry})");
            }

            Console.WriteLine("\n[3] Запит GROUP BY (Статистика тривалості по жанрах):");

            var genreGroups = tracks.GroupBy(t => t.TrackGenre);

            foreach (var group in genreGroups)
            {
                Console.WriteLine($" * Жанр: {group.Key} (Кількість треків: {group.Count()})");
                // Агрегація всередині групи
                int maxDuration = group.Max(t => t.DurationInSeconds);
                Console.WriteLine($"   Максимальна тривалість у жанрі: {maxDuration} сек.");
            }

            Console.WriteLine("\n[4] Запит AGGREGATE (Формування текстового рядка черги відтворення):");

            string playbackQueueText = tracks.Aggregate(
                "ЧЕРГА:", 
                (current, nextTrack) => current + $" -> [{nextTrack.Title}]"
            );

            Console.WriteLine(playbackQueueText);
            Console.WriteLine("\n[5] Тест кастомних методів розширення (Extension Methods):");
            
            var customFiltered = tracks.GetLongTracks().FilterByGenre(Genre.Podcast);
            
            foreach (var track in customFiltered)
            {
                Console.WriteLine($"   [Екстеншн фільтр]: Довгий підкаст -> {track.Title} ({track.DurationInSeconds} сек.)");
            }
        }
    }
}