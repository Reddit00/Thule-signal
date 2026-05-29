using System;
using System.IO;
using NAudio.Wave;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class HardwareAudioOutput : IAudioOutput, IDisposable
    {
        private IWavePlayer? _outputDevice;
        private WaveStream? _audioFile;

        public void InitializeDevice()
        {
            System.Diagnostics.Debug.WriteLine("[Hardware] Аудіо-систему NAudio ініціалізовано.");
        }

        public void PlayStream(Track track)
        {
            if (track == null) return;

            string source = track.GetPlaybackSource();
            CleanUp();

            try
            {
                string finalPath = source;
                if (!File.Exists(finalPath))
                    finalPath = Path.Combine(AppContext.BaseDirectory, source);
                
                if (!File.Exists(finalPath))
                {
                    string projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName ?? "";
                    finalPath = Path.Combine(projectRoot, source);
                }

                if (!File.Exists(finalPath))
                    throw new FileNotFoundException($"Файл не знайдено: {source}");

                string extension = Path.GetExtension(finalPath).ToLower();
                if (extension == ".mp3")
                {
                    _audioFile = new Mp3FileReader(finalPath);
                }
                else if (extension == ".wav")
                {
                    _audioFile = new WaveFileReader(finalPath);
                }
                else
                {
                    throw new NotSupportedException("Підтримуються тільки файли .mp3 та .wav");
                }

                _outputDevice = new WaveOutEvent();
                _outputDevice.Init(_audioFile);
                _outputDevice.Play();
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка NAudio: {ex.Message}");
            }
        }

        public void SetPosition(int seconds)
        {
            if (_audioFile != null)
            {
                long newPosition = _audioFile.WaveFormat.AverageBytesPerSecond * seconds;
                if (newPosition < 0) newPosition = 0;
                if (newPosition > _audioFile.Length) newPosition = _audioFile.Length;

                _audioFile.Position = newPosition;
            }
        }
        public double GetCurrentPositionSeconds()
        {
            return _audioFile?.CurrentTime.TotalSeconds ?? 0;
        }

        public double GetTotalDurationSeconds()
        {
            return _audioFile?.TotalTime.TotalSeconds ?? 0;
        }

        public bool IsPlaying()
        {
            return _outputDevice?.PlaybackState == PlaybackState.Playing;
        }

        public void StopStream()
        {
            _outputDevice?.Stop();
        }

        private void CleanUp()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _outputDevice = null;

            _audioFile?.Dispose();
            _audioFile = null;
        }

        public void SetVolume(int volume)
        {
            if (_outputDevice != null) _outputDevice.Volume = volume / 100f;
        }

        public void Dispose() => CleanUp();
    }
}