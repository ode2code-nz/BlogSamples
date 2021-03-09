﻿using Todo.Domain.Common;

namespace Todo.Domain.Model.StaticData
{
    public class Location : Entity
    {
        public Location(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
