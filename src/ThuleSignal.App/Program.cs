using System;
using ThuleSignal.App.Services;
using ThuleSignal.App.Services.Contracts;      
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.Domain.Entities;
namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ТЕСТ ІВЕНТ-СИСТЕМИ ТА ЗАХИСТУ ПАМ'ЯТІ \n");

            IAudioOutput audioOutput = new HardwareAudioOutput();
            var player = new PlayerEngine(audioOutput);

            var track = new PodcastTrack("id-11", "Event Driven Architecture", 210, "events.mp3", "art-11", "Tech Host");

            Console.WriteLine("[Етап 1] Ініціалізація підписників");
            var mainWindow = new EventUiObserver(player, "Головний Екран UI");
            
            var notificationWindow = new EventUiObserver(player, "Панель Сповіщень");

            Console.WriteLine("\n[Етап 2] Подія: Пуск першого треку");
            player.PlayTrack(track);

            Console.WriteLine("\n[Етап 3] Закриття Панелі Сповіщень (Безпечна відписка)");
            notificationWindow.Unsubscribe(); 

            Console.WriteLine("\n[Етап 4] Подія: Ставимо плеєр на паузу ");
            player.Pause();

            Console.WriteLine("\nТест завершено успішно. Компоненти повністю розв'язані.");
        }
    }
}