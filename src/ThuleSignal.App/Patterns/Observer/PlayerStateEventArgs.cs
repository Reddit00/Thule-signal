using System;

namespace ThuleSignal.App.Patterns.Observer
{
    public class PlayerStateEventArgs : EventArgs
    {
        public string State { get; }
        public string CurrentTrackTitle { get; }
        public PlayerStateEventArgs(string state, string currentTrackTitle)
        {
            State = state;
            CurrentTrackTitle = currentTrackTitle;
        }
    }
}