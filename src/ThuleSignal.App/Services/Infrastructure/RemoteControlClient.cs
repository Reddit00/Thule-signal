using System;
using ThuleSignal.App.Services.Contracts;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class RemoteControlClient
    {
        private readonly IPlaybackControllable _playbackControl;
        public RemoteControlClient(IPlaybackControllable playbackControl)
        {
            _playbackControl = playbackControl ?? throw new ArgumentNullException(nameof(playbackControl));
        }

        public void PressPlayButton()
        {
            Console.WriteLine("[Remote Control] Користувач натиснув кнопку PLAY.");
            _playbackControl.Play();
        }

        public void PressPauseButton()
        {
            Console.WriteLine("[Remote Control] Користувач натиснув кнопку PAUSE.");
            _playbackControl.Pause();
        }
    }
}