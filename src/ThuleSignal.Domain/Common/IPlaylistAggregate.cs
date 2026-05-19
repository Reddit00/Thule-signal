using ThuleSignal.Domain.Entities;

namespace ThuleSignal.Domain.Common
{
    public interface IPlaylistAggregate
    {
        IPlaylistIterator CreateIterator();
    }
}