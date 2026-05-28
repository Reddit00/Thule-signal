using System;

namespace ThuleSignal.Domain.Entities
{
    public abstract class MediaComponent
    {
        public string Title { get; protected set; }
        protected MediaComponent(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
        public abstract void DisplayStructure(int depth);
        public abstract int GetTotalDuration();
    }
}