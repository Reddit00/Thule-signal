using System;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class HardwareAudioOutput : IAudioOutput
    {
        public void InitializeDevice() => Console.WriteLine("[Hardware] Звукова карта ініціалізована через драйвер ASIO.");

        public void PlayStream(Track track)
        {
            Console.WriteLine($"[Hardware] НА ПРЯМУ В ДИНАМІКИ: Пуск звукової хвилі для -> {track.GetPlaybackSource()}");
        }

        public void StopStream() => Console.WriteLine("[Hardware] Сигнал на динаміки зупинено.");
        public void SetVolume(int volume) => Console.WriteLine($"[Hardware] Гучність заліза: {volume}%");
    }
}