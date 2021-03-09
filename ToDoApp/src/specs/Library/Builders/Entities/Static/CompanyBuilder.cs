using ToDo.Domain.Model.StaticData;
using TestStack.Dossier;

namespace Specs.Library.ToDo.Builders.Entities.Static
{
    public class CompanyBuilder : TestDataBuilder<Company, CompanyBuilder>
    {
        public CompanyBuilder()
        {
            Set(x => x.Id, 1);
            Set(x => x.Name, Any.Company.Name);
            Set(x => x.Description, Any.LoremIpsum);
        }
    }
}