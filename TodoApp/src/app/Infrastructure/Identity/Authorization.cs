using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Todo.Infrastructure.Identity
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }

        public const string DefaultPassword = "Pa$$w0rd.";

        public const string UserUserId = "22e40406-8a9d-2d82-912c-5d6a640ee696";
        public const string UserRoleId = "b421e928-0613-9ebd-a64c-f10b6a706e73";

        public const string ModeratorUserId = "7db4adec-3cd2-4c40-80b1-13e791060185";
        public const string ModeratorRoleId = "366d6cdd-7391-45ac-8e29-1f84afbc4b8f";

        public const string AdminUserId = "ed19cc4d-a03b-467a-b714-5fe9e309e605";
        public const string AdminRoleId = "8dd1df58-375b-4277-8284-aeae1e49f0a8";

        public static JwtSecurityToken CreateJwtToken(ApplicationUser user, Jwt jwtSettings,
            IList<string> roles, IList<Claim> userClaims = null)
        {
            userClaims ??= new List<Claim>();
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}