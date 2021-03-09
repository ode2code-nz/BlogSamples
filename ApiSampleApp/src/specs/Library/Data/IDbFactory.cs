using ApiSample.Infrastructure.Data;

namespace Specs.Library.ApiSample.Data
{
    public interface IDbFactory
    {
        AppDbContext CreateContext(bool beginTransaction = false);
        void CreateDatabase();
        void DeleteDatabase();
        void ResetData();
    }
}