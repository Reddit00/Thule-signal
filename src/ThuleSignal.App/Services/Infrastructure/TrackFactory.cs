using System;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public static class TrackFactory
    {
        public static Track CreateTrack(string type, string id, string title, string source, string extraParam)
        {
            if (string.IsNullOrWhiteSpace(type)) 
                throw new ArgumentNullException(nameof(type), "Тип не може бути порожнім.");

            string normalizedType = type.Trim().ToUpper();

            return normalizedType switch
            {
                "PODCAST" => 
                    new PodcastTrack(id, title, 180, source, "art-factory", extraParam),
                
                "STREAM" => 
                    new StreamingTrack(id, title, source, 192),
                
                _ => throw new ArgumentException($"[Factory Error] Невідомий тип медіа-ресурсу: {type}")
            };
        }
    }
}