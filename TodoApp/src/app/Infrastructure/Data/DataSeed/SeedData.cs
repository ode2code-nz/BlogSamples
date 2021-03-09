using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Model.StaticData;
using Todo.Infrastructure.Identity;

namespace Todo.Infrastructure.Data.DataSeed
{
    public static class StaticDataSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Company>().HasData(
                new Company(1, "South Hook Gas", "South Hook Gas", "SHG"),
                new Company(2, "Quatar Gas", "Quatar Gas", "QG"),
                new Company(3, "A2A S.p.A.", "A2A", "A2A"),
                new Company(4, "Abu Dhabi Gas Liquefaction Company Limited", "ADGAS", "ADGAS"),
                new Company(5, "Alpiq AG", "Alpiq AG", "Alpiq AG"),
                new Company(6, "Angola LNG Limited", "ALNG", "ALNG"),
                new Company(7, "Axpo Solutions AG (Axpo Trading AG; Axpo Trading Ltd.)", "Axpo Solutions", "Axpo Solutions"),
                new Company(8, "SHELL GLOBAL LNG LIMITED (SINGAPORE BRANCH)", "SHELL GLOBAL LNG LIMITED SG", "SHELL GLOBAL LNG LIMITED SG"),
                new Company(9, "Bharat Petroleum Corporation Ltd", "Bharat Petroleum Corporation", "Bharat Petroleum Corporation")
            );

            builder.Entity<Location>().HasData(
                new Location(1, "South Hook", "South Hook"),
                new Location(2, "Portland Bight FSRU", "FSU in Jamaica Golar Arctic"),
                new Location(3, "Port Qasim", "Pakistan LNG terminal"),
                new Location(4, "Sabine Pass", "Sabine Pass LNG"),
                new Location(5, "Soyo Angola", "Soyo LNG Terminal"),
                new Location(6, "Dahej", "Dahej"),
                new Location(7, "ETKI FSRU Turkey", "ETKI FSRU Turkey"),
                new Location(8, "Oita", "Oita LNG Terminal"),
                new Location(9, "Ain Sokhna FSRU", "Al-Sokhna Port Egypt")
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Authorization.AdminRoleId, Name = "Administrator" },
                new IdentityRole { Id = Authorization.ModeratorRoleId, Name = "Moderator" },
                new IdentityRole { Id = Authorization.UserRoleId, Name = "User" }
                );

            builder.Entity<ApplicationUser>().HasData(
                ApplicationUser.Create(Authorization.AdminUserId, "admin", "admin@example.com"),
                ApplicationUser.Create(Authorization.ModeratorUserId, "moderator", "moderator@example.com"),
                ApplicationUser.Create(Authorization.UserUserId, "user", "user@example.com")
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = Authorization.AdminRoleId,
                    UserId = Authorization.AdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = Authorization.ModeratorRoleId,
                    UserId = Authorization.ModeratorUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = Authorization.UserRoleId,
                    UserId = Authorization.UserUserId
                }
            );
        }
    }
}