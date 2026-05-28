using ThuleSignal.Domain.Common;

namespace ThuleSignal.Domain.Entities
{
    public class Artist : IEntity
    {
        public string Id { get; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Artist(string id, string name, string country)
        {
            Id = id;
            Name = name;
            Country = country;
        }
    }
}