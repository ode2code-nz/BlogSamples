using Todo.Domain.Model.StaticData;
using TestStack.Dossier;

namespace Specs.Library.Todo.Builders.Entities.Static
{
    public class LocationBuilder : TestDataBuilder<Location, LocationBuilder>
    {
        public LocationBuilder()
        {
            Set(x => x.Id, 1);
            Set(x => x.Name, Any.Company.Name);
            Set(x => x.Description, Any.LoremIpsum);
        }
    }
}