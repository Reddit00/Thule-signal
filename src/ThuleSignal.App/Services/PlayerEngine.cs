using System;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services
{
    public class PlayerEngine : IPlaybackControllable
    {
        public event EventHandler<PlayerStateEventArgs>? PlayerStateChanged;
        private string _currentState = "Stopped";
        private readonly IAudioOutput _audioOutput;
        private Track? _currentTrack;
        public PlayerEngine(IAudioOutput audioOutput)
        {
            _audioOutput = audioOutput ?? throw new ArgumentNullException(nameof(audioOutput));
        }

        public void PlayTrack(Track track)
        {
            _currentTrack = track;
            _currentState = "Playing";
            _audioOutput.PlayStream(_currentTrack);
            
            OnPlayerStateChanged(new PlayerStateEventArgs(_currentState, _currentTrack.Title));
        }

        public void Play() => Console.WriteLine("[Player] Продовження відтворення.");
        public void Pause()
        {
            _currentState = "Paused";
            _audioOutput.StopStream();
            OnPlayerStateChanged(new PlayerStateEventArgs(_currentState, _currentTrack?.Title ?? "Немає треку"));
        }

        protected virtual void OnPlayerStateChanged(PlayerStateEventArgs e)
        {
            PlayerStateChanged?.Invoke(this, e);
        }
    }
}