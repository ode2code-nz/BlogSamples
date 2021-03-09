using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Model.StaticData;

namespace Todo.Domain.Abstractions
{
    public interface IStaticDataRepository
    {
        Task<StaticDataStore> GetStaticDataAsync(CancellationToken token = default);
    }
}
