using System;
using System.IO;
using ThuleSignal.App.Dto;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ЗБЕРЕЖЕННЯ СТАНУ ПРОГРАМИ ЧЕРЕЗ JSON & DTO \n");

            string stateFilePath = "thule_player_state.json";

            Console.WriteLine("[Сесія 1: Робота та Серіалізація] ");
            
            var originalPlaylist = new Playlist("Вечірній Стрім Thule");
            originalPlaylist += new PodcastTrack("id-101", "SOLID Architecture Overview", 300, "solid.mp3", "art-1", "Uncle Bob");
            originalPlaylist += new StreamingTrack("id-102", "Kyiv Electro Beats", "https://stream.ua/electro", 320);

            PlaylistDto playlistDto = MappingService.ToDto(originalPlaylist);

            var persistenceService = new JsonPersistenceService();
            persistenceService.SavePlaylist(playlistDto, stateFilePath);

            Console.WriteLine("\n[Зміст згенерованого файлу JSON]:");
            Console.WriteLine(File.ReadAllText(stateFilePath));

            Console.WriteLine("\n[Сесія 2: Очищення пам'яті та Десеріалізація]");
            
            PlaylistDto loadedDto = persistenceService.LoadPlaylist(stateFilePath);

            Playlist restoredPlaylist = MappingService.ToDomain(loadedDto);

            Console.WriteLine($"\n[Успіх відновлення]: Плейлист '{restoredPlaylist.Name}' знову в ОЗУ.");
            Console.WriteLine($"Кількість відновлених треків: {restoredPlaylist.Tracks.Count} шт.");
            foreach (var track in restoredPlaylist.Tracks)
            {
                Console.WriteLine($" -> Трек: '{track.Title}' | Тип: {track.GetType().Name} | {track.GetPlaybackSource()}");
            }

            if (File.Exists(stateFilePath)) File.Delete(stateFilePath);
        }
    }
}