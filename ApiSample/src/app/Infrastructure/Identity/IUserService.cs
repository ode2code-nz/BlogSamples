using System.Threading.Tasks;
using ApiSample.Infrastructure.Identity.Models;

namespace ApiSample.Infrastructure.Identity
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
