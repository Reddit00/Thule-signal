using System;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class BluetoothAudioOutput : IAudioOutput
    {
        public void InitializeDevice() => Console.WriteLine("[Bluetooth] Пошук пристрою... Навушники 'Thule-Pods' підключено по кодеку aptX HD.");

        public void PlayStream(Track track)
        {
            Console.WriteLine($"[Bluetooth] СТРИМІНГ ПО ПОВІТРЮ: Передача стиснутих аудіо-пакетів -> {track.Title}");
        }

        public void StopStream() => Console.WriteLine("[Bluetooth] Радіопотік розірвано. Сплячий режим.");
        public void SetVolume(int volume) => Console.WriteLine($"[Bluetooth] Цифровий сигнал послаблено до: {volume}%");
    }
}