namespace ThuleSignal.App.Patterns.Observer
{
    public interface IPlayerSubject
    {
        void RegisterObserver(IPlayerObserver observer);
        void RemoveObserver(IPlayerObserver observer);
        void NotifyObservers();
    }
}