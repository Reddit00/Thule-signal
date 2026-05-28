using System;
using System.Collections.Generic;
using ThuleSignal.App.Services;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Common;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ЗАПУСК ПОРОДЖУВАЛЬНИХ ПАТЕРНІВ (ПЗ 10 & СР 10) \n");

            ThuleConfiguration config1 = ThuleConfiguration.Instance;
            ThuleConfiguration config2 = ThuleConfiguration.Instance;

            if (ReferenceEquals(config1, config2))
            {
                Console.WriteLine("[Singleton Check] Успіх: config1 та config2 посилаються на один і той самий об'єкт у пам'яті.\n");
            }

            var inputCliArguments = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { {"Type", "PODCAST"}, {"Id", "id-1"}, {"Title", "SOLID Architecture"}, {"Source", "podcast1.mp3"}, {"Extra", "Uncle Bob"} },
                new Dictionary<string, string> { {"Type", "STREAM"}, {"Id", "id-2"}, {"Title", "Kyiv Dark Ambient Radio"}, {"Source", "https://stream.ua/live"}, {"Extra", "320"} },
                new Dictionary<string, string> { {"Type", config1.DefaultTrackType}, {"Id", "id-3"}, {"Title", "Default Config Track"}, {"Source", "default.mp3"}, {"Extra", "System Host"} }
            };
            var playlist = new Playlist("Сгенерований фабрикою Плейлист");

            Console.WriteLine("--- Фабрика розбирає вхідні конфігурації ---");
            foreach (var rawData in inputCliArguments)
            {
                Track dynamicTrack = TrackFactory.CreateTrack(
                    rawData["Type"],
                    rawData["Id"],
                    rawData["Title"],
                    rawData["Source"],
                    rawData["Extra"]
                );

                playlist += dynamicTrack;
            }

            Console.WriteLine("\n--- Перевірка роботи згенерованих об'єктів у плеєрі ---");
            IAudioOutput audioOutput = new HardwareAudioOutput();
            var player = new PlayerEngine(audioOutput);
            
            player.LoadPlaylist(playlist);
            player.Play();    
            player.NextTrack();  
        }
    }
}