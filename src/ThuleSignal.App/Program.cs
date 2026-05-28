using System;
using ThuleSignal.App.Services;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.App.Patterns.Strategy;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ІНІЦІАЛІЗАЦІЯ СИСТЕМИ THULE-SIGNAL");
           
            var track1 = new PodcastTrack("id-1", "Clean Architecture Concept", 180, "C:/music/alpha.mp3", "Uncle Bob");
            var track2 = new StreamingTrack("id-2", "Thule Echo Radio", "https://stream.thule.com/live", 320);
            var track3 = new PodcastTrack("id-3", "Nordic Frost Podcast", 150, "C:/music/frost.mp3", "Thule Team");

            var playlist = new Playlist("Ембієнт Thule-Signal");
            playlist += track1;
            playlist += track2;
            playlist += track3;

            var audioOutput = new HardwareAudioOutput();
            var player = new PlayerEngine(audioOutput);
            
            var ui = new ConsoleUiObserver();
            player.RegisterObserver(ui);

            player.LoadPlaylist(playlist);

            player.Play();        
            player.NextTrack();   
            player.Pause();       

            player.SetStrategy(new ShuffleStrategy());
            player.NextTrack();   
        }
    }
}