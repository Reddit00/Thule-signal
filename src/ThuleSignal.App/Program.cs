using System;
using System.Collections.Generic;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var artists = new List<Artist>
            {
                new Artist("art-1", "Robert Martin", "USA"),
                new Artist("art-2", "Thule Dark Project", "Ukraine"),
                new Artist("art-3", "Nordic Frost Beats", "Norway")
            };

            var tracks = new List<Track>
            {
                new PodcastTrack("t1", "Clean Code Introduction", 200, "cc1.mp3", "art-1", "Robert Martin"),
                new PodcastTrack("t2", "SOLID Principles Deep Dive", 450, "cc2.mp3", "art-1", "Robert Martin"),
                new PodcastTrack("t3", "KISS and DRY Rules", 90, "cc3.mp3", "art-1", "Robert Martin"),
                new StreamingTrack("t4", "Thule Dark Ambient Stream", "https://stream.io/dark", 320) { ArtistId = "art-2", TrackGenre = Genre.Ambient, DurationInSeconds = 500 },
                new StreamingTrack("t5", "Nordic Frost Winter Ritual", "https://stream.io/nordic", 128) { ArtistId = "art-3", TrackGenre = Genre.Ambient, DurationInSeconds = 120 }
            };

            var analytics = new LinqAnalyticsService();
            analytics.ExecuteAnalytics(tracks, artists);
        }
    }
}