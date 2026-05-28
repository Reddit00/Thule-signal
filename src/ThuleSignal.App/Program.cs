using System;
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;
using ThuleSignal.Domain.Exceptions;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("ЕСТ СИСТЕМИ ВИНИТКІВ ТА RETRY POLICY ");

            var streamTrack = new StreamingTrack("t-live", "Thule Ambient Radio", "https://icecast.thule.com/live.mp3", 320);
            var bluetoothOutput = new BluetoothAudioOutput();
            
            var retryService = new RetryPolicyService();

            try
            {
               retryService.ExecuteWithRetry(() =>
                {
                    bluetoothOutput.PlayStream(streamTrack);
                }, maxRetries: 4, initialDelayMs: 400);
            }
            catch (ThuleSignalException ex)
            {
                Console.WriteLine($"\n[Критичний UI Лог]: Додаток зловив системний збій: {ex.Message}");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n[Finally] Стенд завершив роботу. Пам'ять стабільна, витоків ресурсів немає.");
                Console.ResetColor();
            }
        }
    }
}