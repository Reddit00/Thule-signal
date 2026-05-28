using System;
using System.Collections.Generic;
using ThuleSignal.App.Services;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("ЗАПУСК РЕФАКТОРИНГУ SOLID ");

            IAudioOutput bluetoothOutput = new BluetoothAudioOutput();
            
            var playerEngine = new PlayerEngine(bluetoothOutput);

            var uiObserver = new ConsoleUiObserver();
            playerEngine.RegisterObserver(uiObserver);

            var track1 = new PodcastTrack("id-1", "SRP & OCP Lecture", 120, "lecture1.mp3", "art-1", "Uncle Bob");
            var track2 = new PodcastTrack("id-2", "LSP & ISP Workshop", 150, "lecture2.mp3", "art-1", "Uncle Bob");
            var playlist = new Playlist("SOLID Architecture");
            playlist += track1;
            playlist += track2;

            IQueueNavigable navigator = playerEngine;
            navigator.LoadPlaylist(playlist);

            var remoteControl = new RemoteControlClient(playerEngine);

            remoteControl.PressPlayButton();
            Console.WriteLine();
            remoteControl.PressPauseButton();
        }
    }
}