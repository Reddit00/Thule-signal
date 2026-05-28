using System;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("СТРУКТУРНІ ПАТЕРНИ ТА МАСШТАБУВАННЯ \n");

            Console.WriteLine("[1] Створення деревоподібної медіатеки ");
            
            var rootLibrary = new MediaGroup("Головна Медіатека");
            
            var podcastFolder = new MediaGroup("Подкасти про С#");
            podcastFolder.Add(new PodcastTrack("p1", "Патерн Декоратор", 120, "dec.mp3", "art-1", "Uncle Bob"));
            podcastFolder.Add(new PodcastTrack("p2", "Патерн Композит", 150, "comp.mp3", "art-1", "Uncle Bob"));

            var ambientAlbum = new MediaGroup("Альбом: Thule Echoes");
            var singleTrack = new PodcastTrack("t1", "Intro Signal", 60, "intro.mp3", "art-2", "Thule Dj");
            ambientAlbum.Add(singleTrack);

            rootLibrary.Add(podcastFolder);
            rootLibrary.Add(ambientAlbum);

            rootLibrary.DisplayStructure(0);
            Console.WriteLine($"\n[Composite Result] Загальний час усього дерева медіатеки: {rootLibrary.GetTotalDuration()} сек.\n");

            Console.WriteLine("[2] Запуск системи через Фасад та Декоратор ");
            
            var thuleFacade = new ThulePlayerFacade();
            thuleFacade.InitializeSystem("Центральна Консоль UI");

            thuleFacade.PlayEncryptedTrack(singleTrack);
        }
    }
}