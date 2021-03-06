using System;
using Microsoft.EntityFrameworkCore;
using ApiSample.Domain.Common;
using ApiSample.Infrastructure.Data;

namespace Specs.Library.ApiSample.Data
{
    public interface IDb
    {
        AppDbContext CreateContext(bool beginTransaction = false);
        void ExecuteDbContext(Action<AppDbContext> action);
        T ExecuteDbContext<T>(Func<AppDbContext, T> action);

        long Insert<T>(Entity entity) where T : Entity;
        long Insert<T>(params Entity[] entities) where T : Entity;
        void IdentityInsert<T>(T entity, string tableName) where T : Entity;

        void Update<T>(Entity entity) where T : Entity;
        void Update<T>(params Entity[] entities) where T : Entity;

        DbSet<T> Set<T> () where T :  Entity;

        T Find<T>(int id) where T : Entity;
    }
}