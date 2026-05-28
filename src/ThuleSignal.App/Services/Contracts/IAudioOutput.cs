using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Contracts
{
    public interface IAudioOutput
    {
        void InitializeDevice();
        void PlayStream(Track track);
        void StopStream();
        void SetVolume(int volume);
    }
}