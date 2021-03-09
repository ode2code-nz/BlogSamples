using System;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Caching
{
    public interface ICache
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> addItemFactory);
    }
}