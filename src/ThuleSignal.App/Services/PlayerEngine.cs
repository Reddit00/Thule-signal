using System;
using System.Collections.Generic;
using ThuleSignal.App.Patterns.Iterator;
using ThuleSignal.App.Patterns.Strategy;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services
{
    public class PlayerEngine : IPlayerSubject
    {
        private readonly List<IPlayerObserver> _observers = new();
        private IPlaybackStrategy _strategy = new SequentialStrategy(); 
        
        private Playlist? _currentPlaylist;
        private PlaylistIterator? _iterator;
        private Queue<Track> _playbackQueue = new();
        
        private Track? _currentTrack;
        private string _currentState = "Stopped";

        public void SetStrategy(IPlaybackStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            Console.WriteLine($"[Player] Режим змінено на: {strategy.GetType().Name}");
            RebuildQueue();
        }

        public void LoadPlaylist(Playlist playlist)
        {
            _currentPlaylist = playlist ?? throw new ArgumentNullException(nameof(playlist));
            _iterator = new PlaylistIterator(new List<Track>(playlist.Tracks));
            _currentState = "Playlist Loaded";
            _currentTrack = null;
            RebuildQueue();
            NotifyObservers();
        }

        private void RebuildQueue()
        {
            if (_currentPlaylist == null || _iterator == null) return;
            
            _playbackQueue = _strategy.GenerateQueue(
                new List<Track>(_currentPlaylist.Tracks), 
                _iterator.CurrentIndex
            );
        }

        public void Play()
        {
            if (_currentPlaylist == null || _iterator == null)
            {
                Console.WriteLine("Помилка: Плейлист не завантажено!");
                return;
            }

            if (_currentTrack == null)
            {
                NextTrack();
                return;
            }

            _currentState = "Playing";
            NotifyObservers();
        }

        public void Pause()
        {
            _currentState = "Paused";
            NotifyObservers();
        }

        public void NextTrack()
        {
            if (_playbackQueue.Count > 0)
            {
                _currentTrack = _playbackQueue.Dequeue();
                _currentState = "Playing";
                NotifyObservers();
                return;
            }

            if (_iterator != null && _iterator.HasNext())
            {
                _currentTrack = _iterator.Next();
                _currentState = "Playing";
                RebuildQueue();
                NotifyObservers();
            }
            else
            {
                _currentState = "End of Playlist";
                _currentTrack = null;
                NotifyObservers();
            }
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