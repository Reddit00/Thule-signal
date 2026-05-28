using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App.Services.Contracts
{
    public interface IQueueNavigable
    {
        void NextTrack();
        void LoadPlaylist(Playlist playlist);
    }
}