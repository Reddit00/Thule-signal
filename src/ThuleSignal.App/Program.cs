using System;
using System.Collections.Generic;
using ThuleSignal.App.Common;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ТЕСТ УЗАГАЛЬНЕНЬ ТА ДЕЛЕГАТІВ У СХОВИЩІ ТРЕКІВ\n");

            IRepository<Track> trackRepository = new InMemoryRepository<Track>();

            var track1 = new PodcastTrack("t1", "Clean Code Introduction", 200, "cc1.mp3", "Robert Martin");
            var track2 = new StreamingTrack("t2", "Thule Dark Ambient Stream", "https://stream.io/dark", 320);
            var track3 = new PodcastTrack("t3", "SOLID Principles", 450, "cc2.mp3", "Robert Martin");

            trackRepository.Add(track1);
            trackRepository.Add(track2);
            trackRepository.Add(track3);

            IEnumerable<Track> allTracks = trackRepository.GetAll();

            Console.WriteLine("\n--- Виклик ForEach (Джерела відтворення) ---");
            GenericDataProcessor.ForEach(allTracks, t => Console.WriteLine($">> {t.Title}: {t.GetPlaybackSource()}"));

            
            Console.WriteLine("\n--- Виклик Map (Трансформація в імена) ---");
            IEnumerable<string> trackTitles = GenericDataProcessor.Map(allTracks, t => t.Title.ToUpper());
            foreach (var title in trackTitles)
            {
                Console.WriteLine($"[Mapped Title]: {title}");
            }

            Console.WriteLine("\n--- Виклик Reduce (Агрегація даних) ---");
            int totalDuration = GenericDataProcessor.Reduce(allTracks, 0, (sum, track) => sum + track.DurationInSeconds);
            Console.WriteLine($"[Reduced Result] Загальний час усього сховища: {totalDuration} секунд.");
        }
    }
}