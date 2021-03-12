using System.Threading;
using System.Threading.Tasks;
using ApiSample.Domain.Model.StaticData;

namespace ApiSample.Domain.Abstractions
{
    public interface IStaticDataRepository
    {
        Task<StaticDataStore> GetStaticDataAsync(CancellationToken token = default);
    }
}
