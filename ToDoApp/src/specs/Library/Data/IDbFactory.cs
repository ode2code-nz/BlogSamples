using ToDo.Infrastructure.Data;

namespace Specs.Library.ToDo.Data
{
    public interface IDbFactory
    {
        AppDbContext CreateContext(bool beginTransaction = false);
        void CreateDatabase();
        void DeleteDatabase();
        void ResetData();
    }
}