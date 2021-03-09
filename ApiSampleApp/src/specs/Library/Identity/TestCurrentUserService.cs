using ToDo.Infrastructure.Interfaces;

namespace Specs.Library.ToDo.Identity
{
    public class TestCurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
    }
}