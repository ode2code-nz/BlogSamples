using Todo.Infrastructure.Interfaces;

namespace Specs.Library.Todo.Identity
{
    public class TestCurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
    }
}