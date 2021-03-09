using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using DbMigrator;
using ToDo.Infrastructure.Data;


namespace Specs.Library.ToDo.Data.SqlServer
{
    public class DbUpSqlDbFactory : EfSqlDbFactory
    {
        public DbUpSqlDbFactory(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public override void CreateDatabase()
        {
            var sqlServerOptionsExtension = DbOptions.FindExtension<SqlServerOptionsExtension>();
            var connectionString = sqlServerOptionsExtension.ConnectionString;
            Db.Migrate(connectionString, true);
        }
    }
}