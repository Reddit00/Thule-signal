using System;
using ThuleSignal.App.Services;

namespace ThuleSignal.App.Patterns.Observer
{
    public class EventUiObserver : IDisposable
    {
        private readonly PlayerEngine _player;
        private readonly string _uiInstanceName;
        public EventUiObserver(PlayerEngine player, string uiName)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _uiInstanceName = uiName;

            _player.PlayerStateChanged += OnPlayerStateChanged;
            Console.WriteLine($"[{_uiInstanceName}] Успішно підключено до подій плеєра.");
        }
        private void OnPlayerStateChanged(object? sender, PlayerStateEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"СИСТЕМНЕ ВІКНО ({_uiInstanceName}) ");
            Console.WriteLine($"Статус: [{e.State}] | Грає трек: {e.CurrentTrackTitle}");
            Console.ResetColor();
        }

        public void Unsubscribe()
        {
            _player.PlayerStateChanged -= OnPlayerStateChanged;
            Console.WriteLine($"[{_uiInstanceName}] Відключено від подій. Ризик витоку пам'яті ліквідовано.");
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}