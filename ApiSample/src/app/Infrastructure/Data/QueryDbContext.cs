using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiSample.Domain.Common;
using ApiSample.Domain.Model.ToDos;
using ApiSample.Infrastructure.Interfaces;

namespace ApiSample.Infrastructure.Data
{
    public class QueryDbContext : IQueryDb
    {
        private readonly AppDbContext _db;

        public QueryDbContext(AppDbContext db)
        {
            _db = db;
            db.ChangeTracker.AutoDetectChangesEnabled = false; // is this needed with QueryTrackingBehavior?
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public IQueryable<ToDoItem> ToDoItems => _db.ToDoItems;

        // This is a generic alternative to above. Team should discuss preference.
        public IQueryable<T> QueryFor<T>() where T : Entity
        {
            return _db.Set<T>();
        }
    }
}