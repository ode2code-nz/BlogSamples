using System.Collections.Generic;

namespace Todo.Domain.Model.StaticData
{
    public class StaticDataStore
    {
        public Dictionary<int, Company> Companies { get; set; }
        public Dictionary<int, Location> Locations { get; set; }
    }
}
