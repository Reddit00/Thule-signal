using System;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;
using ThuleSignal.Domain.Exceptions;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class BluetoothAudioOutput : IAudioOutput
    {
        private static int _simulatedNetworkCrashCount = 0;

        public void InitializeDevice() => Console.WriteLine("[Bluetooth] Навушники підключено.");
        public void StopStream() => Console.WriteLine("[Bluetooth] Потік зупинено.");
        public void SetVolume(int volume) { }

        public void PlayStream(Track track)
        {
            using (track)
            {
                Console.WriteLine($"[Audio Engine] Спроба відкрити потік для: {track.Title}");

                if (track is StreamingTrack stream)
                {
                    _simulatedNetworkCrashCount++;
                    if (_simulatedNetworkCrashCount <= 2) 
                    {
                        throw new NetworkStreamingException(
                            "Помилка буферизації: Мережа нестабільна (Код 503)", 
                            stream.StreamUrl
                        );
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Bluetooth] УСПІШНО ГРАЄ: -> {track.Title}");
                Console.ResetColor();
            } 
        }
    }
}