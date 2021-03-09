using Todo.Infrastructure.Data;

namespace Specs.Library.Todo.Data
{
    public interface IDbFactory
    {
        AppDbContext CreateContext(bool beginTransaction = false);
        void CreateDatabase();
        void DeleteDatabase();
        void ResetData();
    }
}