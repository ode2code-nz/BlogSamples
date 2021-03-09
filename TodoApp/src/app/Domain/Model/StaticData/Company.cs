using ToDo.Domain.Common;

namespace ToDo.Domain.Model.StaticData
{
    public class Company : Entity
    {
        public Company(int id, string name, string description, string code)
        {
            Id = id;
            Name = name;
            Description = description;
            Code = code;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
