using Todo.Domain.Model.StaticData;
using Specs.Library.Todo.Builders.Entities.Static;

namespace Specs.Library.Todo.Builders.ObjectMothers
{
    public class StaticDataMother
    {
        private static Location _location = new LocationBuilder();
        public Location Location => _location;

        private static Company _company = new CompanyBuilder();
        public Company Company => _company;
    }
}
