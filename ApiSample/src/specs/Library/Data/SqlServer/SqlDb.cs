using System;
using Microsoft.EntityFrameworkCore;
using ApiSample.Domain.Common;
using ApiSample.Infrastructure.Data;
using Specs.Library.ApiSample.Helpers;

namespace Specs.Library.ApiSample.Data.SqlServer
{
    public class SqlDb : IDb
    {
        private readonly IDbFactory _dbFactory;

        public SqlDb(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public AppDbContext CreateContext(bool beginTransaction = false)
        {
            return _dbFactory.CreateContext(beginTransaction);
        }

        public void ExecuteDbContext(Action<AppDbContext> action)
        {
            var dbContext = CreateContext();
            var strategy = dbContext.CreateExecutionStrategy();

            AsyncHelper.RunSync(() => strategy.ExecuteAsync(async () =>
            {

                try
                {
                    AsyncHelper.RunSync(() => dbContext.BeginTransactionAsync());
                    action(dbContext);
                    AsyncHelper.RunSync(() => dbContext.CommitTransactionAsync());
                }
                catch (Exception exception)
                {
                    dbContext.RollbackTransaction();
                    throw;
                }
            }));
        }

        public T ExecuteDbContext<T>(Func<AppDbContext, T> action)
        {
            var dbContext = CreateContext();
            var strategy = dbContext.CreateExecutionStrategy();

            return AsyncHelper.RunSync(() => strategy.ExecuteAsync(async () =>
            {
                try
                {
                    AsyncHelper.RunSync(() => dbContext.BeginTransactionAsync());
                    var result = action(dbContext);
                    AsyncHelper.RunSync(() => dbContext.CommitTransactionAsync());
                    return result;
                }
                catch (Exception)
                {
                    dbContext.RollbackTransaction();
                    throw;
                }
            }));
        }

        public long Insert<T>(Entity entity) where T : Entity
        {
            return ExecuteDbContext(db =>
            {
                entity.WithEntityId(0);
                db.Set<T>().Attach((T)entity);
                db.SaveChanges();
                return entity.Id;
            });
        }

        public long Insert<T>(params Entity[] entities) where T : Entity
        {
            return ExecuteDbContext(db =>
            {
                foreach (var entity in entities)
                {
                    entity.WithEntityId(0);
                    db.Set<T>().Attach((T)entity);
                }
                return db.SaveChanges();
            });
        }

        public void IdentityInsert<T>(T entity, string tableName) where T : Entity
        {
            using (var db = CreateContext())
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Added;

                db.Database.OpenConnection();
                try
                {
                    string identityOn = $"SET IDENTITY_INSERT {tableName} ON";
                    db.Database.ExecuteSqlRaw(identityOn);
                    db.SaveChanges();
                    string identityOff = $"SET IDENTITY_INSERT {tableName} OFF";
                    db.Database.ExecuteSqlRaw(identityOff);
                }
                finally
                {
                    db.Database.CloseConnection();
                }
            }
        }

        public void Update<T>(Entity entity) where T : Entity
        {
            ExecuteDbContext(db =>
            {
                db.Set<T>().Update((T)entity);
                db.SaveChanges();
            });
        }

        public void Update<T>(params Entity[] entities) where T : Entity
        {
            ExecuteDbContext(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Update((T)entity);
                    db.SaveChanges();
                }
            });
        }

        public void Remove<T>(Entity entity) where T : Entity
        {
            ExecuteDbContext(db =>
            {
                db.Set<T>().Remove((T)entity);
                db.SaveChanges();
            });
        }

        public void Remove<T>(params Entity[] entities) where T : Entity
        {
            ExecuteDbContext(db =>
            {
                db.Set<T>().RemoveRange((T[])entities);
                db.SaveChanges();
            });
        }

        public DbSet<T> Set<T>() where T : Entity
        {
            return ExecuteDbContext(db => db.Set<T>());
        }

        public T Find<T>(int id) where T : Entity
        {
            return ExecuteDbContext(db => db.Set<T>().Find(id));
        }
    }
}