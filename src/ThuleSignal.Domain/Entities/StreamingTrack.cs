using System;

namespace ThuleSignal.Domain.Entities
{
    public class StreamingTrack : Track
    {
        public string StreamUrl => FilePath;
        public int MaxBitrateKbps { get; set; }

        public StreamingTrack(string id, string title, string streamUrl, int maxBitrateKbps) 
            : base(id, title, 1, streamUrl) 
        {
            MaxBitrateKbps = maxBitrateKbps;
        }

        public override string GetPlaybackSource()
        {
            return $"Мережевий потік (URL: {StreamUrl}, Якість: {MaxBitrateKbps} kbps)";
        }
    }
}