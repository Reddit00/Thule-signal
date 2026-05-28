using System;

namespace ThuleSignal.App.Patterns.Observer
{
    public class ConsoleUiObserver : IPlayerObserver
    {
        public void Update(string state, string currentTrackTitle)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nTHULE-SIGNAL UI");
            Console.WriteLine($" Стан плеєра: [{state.ToUpper()}]");
            Console.WriteLine($" Зараз грає : {currentTrackTitle}");
            Console.ResetColor();
        }
    }
}