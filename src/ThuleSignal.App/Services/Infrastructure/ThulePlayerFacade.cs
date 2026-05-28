using System;
using ThuleSignal.App.Patterns.Observer;
using ThuleSignal.App.Services.Contracts;     
using ThuleSignal.App.Services.Infrastructure; 
using ThuleSignal.Domain.Common;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class ThulePlayerFacade
    {
        private readonly PlayerEngine _player;
        private readonly IAudioOutput _audioOutput; 
        private EventUiObserver? _uiObserver;

        public ThulePlayerFacade()
        {
            _audioOutput = new HardwareAudioOutput();
            _player = new PlayerEngine(_audioOutput);
            
            var config = ThuleConfiguration.Instance;
            config.DefaultTrackType = "PREMIUM_STREAM";
        }

        public void InitializeSystem(string uiName)
        {
            _uiObserver = new EventUiObserver(_player, uiName);
            Console.WriteLine("[Facade] Усі внутрішні підсистеми Thule-Signal успішно змонтовані та уніфіковані.");
        }

        public void PlayEncryptedTrack(Track rawTrack)
        {
            Console.WriteLine("\n[Facade] Запит на відтворення захищеного контенту");
            
            Track secureTrack = new EncryptedTrackDecorator(rawTrack);
            
            _player.PlayTrack(secureTrack);
        }
    }
}