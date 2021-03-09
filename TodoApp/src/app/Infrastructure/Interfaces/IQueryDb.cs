using System.Linq;
using Todo.Domain.Common;
using Todo.Domain.Model.ToDos;

namespace Todo.Infrastructure.Interfaces
{
    // This is used by Query Handlers in CQRS
    public interface IQueryDb 
    {
        IQueryable<ToDoItem> ToDoItems { get; }

        // This is a generic alternative to above. Team should discuss preference.
        IQueryable<T> QueryFor<T>() where T : Entity;
    }
}