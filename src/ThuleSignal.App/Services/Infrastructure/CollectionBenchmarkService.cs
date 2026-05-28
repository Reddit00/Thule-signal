using System;
using System.Collections.Generic;
using System.Diagnostics;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class CollectionBenchmarkService
    {
        public void RunBenchmark(int elementCount)
        {
            Console.WriteLine($"ЗАПУСК БЕНЧМАРКУ ДЛЯ {elementCount} ЕЛЕМЕНТІВ");

            var list = new List<Track>();
            var dictionary = new Dictionary<string, Track>();
            var hashSet = new HashSet<Track>();
            var testTracks = new List<Track>();
            for (int i = 0; i < elementCount; i++)
            {
                testTracks.Add(new PodcastTrack($"id-{i}", $"Track Number {i}", 180, $"file-{i}.mp3", "Author"));
            }

            string targetId = $"id-{elementCount - 5}";
            Track targetTrack = testTracks[elementCount - 5];

            var stopwatch = new Stopwatch();
            Console.WriteLine("\n[1] Тестування швидкості вставки даних");

            stopwatch.Restart();
            foreach (var track in testTracks) list.Add(track);
            stopwatch.Stop();
            long listInsertTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> List<T>: {listInsertTime} ms");

            stopwatch.Restart();
            foreach (var track in testTracks) dictionary.Add(track.Id, track);
            stopwatch.Stop();
            long dictInsertTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> Dictionary<K, V>: {dictInsertTime} ms");

            stopwatch.Restart();
            foreach (var track in testTracks) hashSet.Add(track);
            stopwatch.Stop();
            long hashInsertTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> HashSet<T>: {hashInsertTime} ms");

            Console.WriteLine("\n[2] Тестування швидкості пошуку елемента...");
            int searchIterations = 10000;

            stopwatch.Restart();
            for (int i = 0; i < searchIterations; i++)
            {
                var found = list.Find(t => t.Id == targetId);
            }
            stopwatch.Stop();
            long listSearchTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> List<T> (Пошук перебором): {listSearchTime} ms");

            stopwatch.Restart();
            for (int i = 0; i < searchIterations; i++)
            {
                var found = dictionary[targetId];
            }
            stopwatch.Stop();
            long dictSearchTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> Dictionary<K, V> (За ключем): {dictSearchTime} ms");

            stopwatch.Restart();
            for (int i = 0; i < searchIterations; i++)
            {
                var found = hashSet.Contains(targetTrack);
            }
            stopwatch.Stop();
            long hashSearchTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"-> HashSet<T> (.Contains): {hashSearchTime} ms");
        }

    }
}