using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Abstractions;
using Todo.Domain.Model.StaticData;

namespace Todo.Infrastructure.Data.Repositories
{
    public class StaticDataRepository : IStaticDataRepository
    {
        private readonly AppDbContext _db;

        public StaticDataRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<StaticDataStore> GetStaticDataAsync(CancellationToken token)
        {
            return new StaticDataStore
            {
                Companies = await _db.Companies.ToDictionaryAsync(x => x.Id, x => x, cancellationToken: token),
                Locations = await _db.Locations.ToDictionaryAsync(x => x.Id, x => x, cancellationToken: token),
            };
        }
    }
}