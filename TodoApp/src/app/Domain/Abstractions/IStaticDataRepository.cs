using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Model.StaticData;

namespace ToDo.Domain.Abstractions
{
    public interface IStaticDataRepository
    {
        Task<StaticDataStore> GetStaticDataAsync(CancellationToken token = default);
    }
}
