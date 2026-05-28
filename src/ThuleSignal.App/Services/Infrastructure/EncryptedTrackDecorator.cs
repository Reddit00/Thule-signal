using System;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class EncryptedTrackDecorator : Track
    {
        private readonly Track _innerTrack; 
        public EncryptedTrackDecorator(Track track) 
            : base(track.Id, track.Title, track.DurationInSeconds, track.FilePath)
        {
            _innerTrack = track ?? throw new ArgumentNullException(nameof(track));
        }

        public override string GetPlaybackSource()
        {
            
            string originalSource = _innerTrack.GetPlaybackSource();
            return $"[DRM ЗАХИСТ: AES-256] -> {originalSource} (Потік дешифровано в ОЗУ)";
        }
    }
}