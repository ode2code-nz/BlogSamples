using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ApiSample.Infrastructure.Identity.Entities;

namespace ApiSample.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public static ApplicationUser Create(string id, string userName, string email, 
            string password = Authorization.DefaultPassword)
        {
            var appUser = new ApplicationUser
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true
            };

            var ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, password);

            return appUser;
        }
    }
}
