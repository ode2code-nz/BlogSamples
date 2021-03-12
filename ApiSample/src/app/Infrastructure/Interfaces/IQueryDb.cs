using System.Linq;
using ApiSample.Domain.Common;
using ApiSample.Domain.Model.ToDos;

namespace ApiSample.Infrastructure.Interfaces
{
    // This is used by Query Handlers in CQRS
    public interface IQueryDb 
    {
        IQueryable<ToDoItem> ToDoItems { get; }

        // This is a generic alternative to above. Team should discuss preference.
        IQueryable<T> QueryFor<T>() where T : Entity;
    }
}