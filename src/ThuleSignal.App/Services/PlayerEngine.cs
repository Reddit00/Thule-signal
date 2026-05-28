using System;
using System.Collections.Generic;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.App.Patterns.Strategy;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services
{
    public class PlayerEngine : IPlaybackControllable, IQueueNavigable, IPlayerSubject
    {
        private readonly List<IPlayerObserver> _observers = new();
        private string _currentState = "Stopped";
        private readonly IAudioOutput _audioOutput;
        
        private IPlaybackStrategy _strategy = new SequentialStrategy(); 
        private Playlist? _currentPlaylist;
        private int _currentIndex = -1;
        private Track? _currentTrack;

        public PlayerEngine(IAudioOutput audioOutput)
        {
            _audioOutput = audioOutput ?? throw new ArgumentNullException(nameof(audioOutput));
        }

        public void SetStrategy(IPlaybackStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            NotifyObservers();
        }

        public void LoadPlaylist(Playlist playlist)
        {
            _currentPlaylist = playlist ?? throw new ArgumentNullException(nameof(playlist));
            _currentIndex = 0;
            _currentState = "Playlist Loaded";
            NotifyObservers();
        }

        public void Play()
        {
            if (_currentPlaylist == null || _currentPlaylist.Tracks.Count == 0) return;
            
            if (_currentTrack == null)
            {
                _currentTrack = _currentPlaylist.Tracks[_currentIndex];
            }

            _currentState = "Playing";
            _audioOutput.PlayStream(_currentTrack); 
            NotifyObservers(); 
        }

        public void Pause()
        {
            _currentState = "Paused";
            _audioOutput.StopStream();
            NotifyObservers();
        }

        public void NextTrack()
        {
            if (_currentPlaylist == null) return;

            _currentIndex++;
            if (_currentIndex < _currentPlaylist.Tracks.Count)
            {
                _currentTrack = _currentPlaylist.Tracks[_currentIndex];
                _currentState = "Playing";
                _audioOutput.PlayStream(_currentTrack);
            }
            else
            {
                _currentState = "End of Playlist";
                _currentTrack = null;
                _audioOutput.StopStream();
            }
            NotifyObservers();
        }

        public void RegisterObserver(IPlayerObserver observer) => _observers.Add(observer);
        public void RemoveObserver(IPlayerObserver observer) => _observers.Remove(observer);
        
        public void NotifyObservers()
        {
            string trackTitle = _currentTrack != null ? _currentTrack.Title : "Немає треку";
            foreach (var observer in _observers)
            {
                observer.Update(_currentState, trackTitle);
            }
        }
    }
}