using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;
using Respawn;

namespace Specs.Library.Todo.Data.SqlServer
{
    /// <summary>
    /// SQL Server database provider
    /// </summary>
    public class EfSqlDbFactory : IDbFactory
    {
        protected readonly DbContextOptions<AppDbContext> DbOptions;
        private Checkpoint _checkpoint;

        public EfSqlDbFactory(DbContextOptions<AppDbContext> dbContextOptions)
        {
            DbOptions = dbContextOptions;

            _checkpoint = new Checkpoint
            {
                DbAdapter = DbAdapter.SqlServer,
                // ReSpawn will delete data from every table not in this white list
                TablesToIgnore = new[] {
                    "SchemaVersions",        // the DbUp migration history table
                    "AspNetRoleClaims",
                    "AspNetRoles",
                    "AspNetUserClaims",
                    "AspNetUserLogins",
                    "AspNetUserRoles",
                    "AspNetUsers",
                    "AspNetUserTokens",
                    "Company",
                    "Location"
                }
            };
        }

        public AppDbContext CreateContext(bool beginTransaction = false)
        {
            var context = new AppDbContext(DbOptions);
            if (beginTransaction)
                context.Database.BeginTransaction();
            return context;
        }

        public virtual void CreateDatabase()
        {
            using (var context = CreateContext(beginTransaction: false))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            };
        }

        public void DeleteDatabase()
        {
            using (var context = CreateContext(beginTransaction: false))
            {
                context.Database.EnsureDeleted();
            }
        }

        public void ResetData()
        {
            using (var connection = CreateContext(beginTransaction: false).Database.GetDbConnection())
            {
                connection.Open();
                _checkpoint.Reset(connection).Wait();
            }
        }
    }
}