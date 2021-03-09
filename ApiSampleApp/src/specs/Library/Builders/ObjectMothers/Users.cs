using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using ToDo.Infrastructure.Identity;
using ToDo.Infrastructure.Identity.Models;

namespace Specs.Library.ToDo.Builders.ObjectMothers
{
    public class Users
    {
        public AuthenticationModel Administrator
        {
            get
            {
                var user = ApplicationUser.Create(Authorization.AdminUserId, "admin", "admin@example.com");//,
                var token = Authorization.CreateJwtToken(user, TestSettings.AppSettings.Jwt,
                    new List<string> {"Administrator"});
                var model = new AuthenticationModel
                {
                    IsAuthenticated = true,
                    UserName = "admin",
                    Email = "admin@example.com",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = "Uj/BUBEGRiehlyL/ncTkemkLUQ0fdwCWvjP/NPpIMVo=",
                };
                model.Roles.Add("Administrator");
                return model;
            }
        }

        public AuthenticationModel Moderator
        {
            get
            {
                var user = ApplicationUser.Create(Authorization.AdminUserId, "moderator", "moderator@example.com");//,
                var token = Authorization.CreateJwtToken(user, TestSettings.AppSettings.Jwt,
                    new List<string> { "Moderator" });
                var model = new AuthenticationModel
                {
                    IsAuthenticated = true,
                    UserName = "moderator",
                    Email = "moderator@example.com",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = "Uj/BUBEGRiehlyL/ncTkemkLUQ0fdwCWvjP/NPpIMVo=",
                };
                model.Roles.Add("Moderator");
                return model;
            }
        }

        public AuthenticationModel User
        {
            get
            {
                var user = ApplicationUser.Create(Authorization.AdminUserId, "user", "user@example.com");//,
                var token = Authorization.CreateJwtToken(user, TestSettings.AppSettings.Jwt,
                    new List<string> { "Administrator" });
                var model = new AuthenticationModel
                {
                    IsAuthenticated = true,
                    UserName = "user",
                    Email = "user@example.com",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = "Uj/BUBEGRiehlyL/ncTkemkLUQ0fdwCWvjP/NPpIMVo=",
                };
                model.Roles.Add("User");
                return model;
            }
        }

        public AuthenticationModel Unauthorized
        {
            get
            {
                var user = new AuthenticationModel
                {
                    IsAuthenticated = false,
                    UserName = "unauthorized",
                    Email = "unauthorized@example.com",
                    Token = null,
                    RefreshToken = null,
                };

                return user;
            }
        }
    }
}
