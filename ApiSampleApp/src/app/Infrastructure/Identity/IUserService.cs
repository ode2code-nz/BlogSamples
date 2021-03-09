using System.Threading.Tasks;
using ToDo.Infrastructure.Identity.Models;

namespace ToDo.Infrastructure.Identity
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        ApplicationUser GetById(string id);

        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);
        bool RevokeToken(string token);
    }
}
