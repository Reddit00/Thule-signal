namespace ThuleSignal.App.Services.Contracts
{
    public interface IAudioOutput
    {
        void InitializeDevice();
        void PlayStream(ThuleSignal.Domain.Entities.Track track);
        void StopStream();
        void SetVolume(int volume);
        void SetPosition(int seconds);
        double GetCurrentPositionSeconds(); 
        double GetTotalDurationSeconds();   
        bool IsPlaying();                
    }
}