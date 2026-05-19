using ThuleSignal.Domain.Entities; // <-- ПЕРЕВІРТЕ ЦЕЙ РЯДОК

namespace ThuleSignal.Domain.Common
{
    public interface IPlaylistIterator
    {
        bool HasNext();
        Track Next(); 
        int CurrentIndex { get; }
    }
}