using ToDo.Domain.Model.StaticData;
using Specs.Library.ToDo.Builders.Entities.Static;

namespace Specs.Library.ToDo.Builders.ObjectMothers
{
    public class StaticDataMother
    {
        private static Location _location = new LocationBuilder();
        public Location Location => _location;

        private static Company _company = new CompanyBuilder();
        public Company Company => _company;
    }
}
