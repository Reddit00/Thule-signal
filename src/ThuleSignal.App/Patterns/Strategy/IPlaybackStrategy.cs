using System;
using System.Collections.Generic;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Patterns.Strategy
{
    // 1. Інтерфейс
    public interface IPlaybackStrategy
    {
        Queue<Track> GenerateQueue(List<Track> tracks, int currentTrackIndex);
    }

    // 2. Послідовна стратегія
    public class SequentialStrategy : IPlaybackStrategy
    {
        public Queue<Track> GenerateQueue(List<Track> tracks, int currentTrackIndex)
        {
            var queue = new Queue<Track>();
            for (int i = currentTrackIndex + 1; i < tracks.Count; i++)
            {
                queue.Enqueue(tracks[i]);
            }
            return queue;
        }
    }

    // 3. Випадкова стратегія (ДОДАЄМО СЮДИ)
    public class ShuffleStrategy : IPlaybackStrategy
    {
        private static readonly Random _random = new Random();

        public Queue<Track> GenerateQueue(List<Track> tracks, int currentTrackIndex)
        {
            var shuffleList = new List<Track>();
            for (int i = 0; i < tracks.Count; i++)
            {
                if (i != currentTrackIndex)
                {
                    shuffleList.Add(tracks[i]);
                }
            }

            int n = shuffleList.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                var value = shuffleList[k];
                shuffleList[k] = shuffleList[n];
                shuffleList[n] = value;
            }

            return new Queue<Track>(shuffleList);
        }
    }
}