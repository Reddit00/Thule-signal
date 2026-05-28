using System;
using ThuleSignal.Domain.Common;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public static class TrackFactory
    {
        public static Track CreateTrack(string type, string id, string title, string source, string extraParam)
        {
            if (string.IsNullOrWhiteSpace(type)) 
                throw new ArgumentNullException(nameof(type), "Тип медіа-ресурсу не може бути порожнім.");

            string normalizedType = type.Trim().ToUpper();

            return normalizedType switch
            {
                SystemConstants.PodcastType => 
                    new PodcastTrack(id, title, 180, source, "art-factory", extraParam),
                
                SystemConstants.StreamType => 
                    new StreamingTrack(id, title, source, int.TryParse(extraParam, out int bitrate) ? bitrate : SystemConstants.DefaultBufferBitrateKbps),
                
                _ => throw new ArgumentException($"[Factory Error] Невідомий тип медіа-ресурсу: {type}")
            };
        }
    }
}