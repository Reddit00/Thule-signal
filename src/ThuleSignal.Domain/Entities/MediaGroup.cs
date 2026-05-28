using System;
using System.Collections.Generic;

namespace ThuleSignal.Domain.Entities
{
    public class MediaGroup : MediaComponent
    {
        private readonly List<MediaComponent> _children = new();
        public MediaGroup(string title) : base(title) { }
        public void Add(MediaComponent component) => _children.Add(component);
        public void Remove(MediaComponent component) => _children.Remove(component);

        public override void DisplayStructure(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} [+ Група: {Title}]");
            foreach (var component in _children)
            {
                component.DisplayStructure(depth + 2); 
            }
        }

        public override int GetTotalDuration()
        {
            int total = 0;
            foreach (var component in _children)
            {
                total += component.GetTotalDuration();
            }
            return total;
        }
    }
}