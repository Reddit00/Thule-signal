using System.Collections.Generic;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Patterns.Strategy
{
   
    public class SequentialPlaybackStrategy : IPlaybackStrategy
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
}