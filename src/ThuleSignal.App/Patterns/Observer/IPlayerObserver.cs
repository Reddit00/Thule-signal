namespace ThuleSignal.App.Patterns.Observer
{
    public interface IPlayerObserver
    {
        void Update(string state, string currentTrackTitle);
    }
}