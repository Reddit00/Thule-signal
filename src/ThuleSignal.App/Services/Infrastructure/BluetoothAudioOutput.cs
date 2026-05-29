using System;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class BluetoothAudioOutput : IAudioOutput
    {
        public void InitializeDevice()
        {
            System.Diagnostics.Debug.WriteLine("[Bluetooth] Беспроводной модуль инициализирован.");
        }

        public void PlayStream(Track track)
        {
            System.Diagnostics.Debug.WriteLine($"[Bluetooth] Потоковое воспроизведение через Bluetooth: {track?.Title}");
        }

        public void StopStream()
        {
            System.Diagnostics.Debug.WriteLine("[Bluetooth] Воспроизведение остановлено.");
        }

        public void SetVolume(int volume)
        {
            System.Diagnostics.Debug.WriteLine($"[Bluetooth] Громкость измнена на: {volume}%");
        }

        public void SetPosition(int seconds)
        {
            System.Diagnostics.Debug.WriteLine($"[Bluetooth] Перемотка внутри Bluetooth-потока на: {seconds} сек.");
        }
        public double GetCurrentPositionSeconds() => 0;
        public double GetTotalDurationSeconds() => 0;
        public bool IsPlaying() => false;
    }
}