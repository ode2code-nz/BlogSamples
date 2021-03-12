using ApiSample.Infrastructure.Interfaces;

namespace Specs.Library.ApiSample.Identity
{
    public class TestCurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
    }
}