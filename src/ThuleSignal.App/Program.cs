using System;
using ThuleSignal.App.Services;
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

            // 1. Створення треків (Демонстрація ПЗ 1: Конструктори з параметрами)
            var track1 = new Track("id-1", "Signal Alpha", 180, "C:/music/alpha.mp3");
            var track2 = new Track("id-2", "Thule Echo", 210, "C:/music/echo.mp3");
            var track3 = new Track("id-3", "Nordic Frost", 150, "C:/music/frost.mp3");

            Console.WriteLine("\n--- Тест копіювання об'єкта ---");
            var duplicateTrack = new Track(track3);
            Console.WriteLine("\n--- Формування плейлиста ---");
            var playlist = new Playlist("Ембієнт Thule-Signal");
            
            playlist += track1;
            playlist += track2;
            playlist += track3;
            playlist += duplicateTrack;

            Console.WriteLine("\n--- Тест індексаторів ---");
            Console.WriteLine($"[Indexer] Трек під індексом 1: {playlist[1].Title}");
            Console.WriteLine($"[Indexer] Пошук за назвою 'Nordic Frost': {playlist["Nordic Frost"].FilePath}");
            Console.WriteLine("\n--- Запуск плеєра та Observer ---");
            var player = new PlayerEngine();
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