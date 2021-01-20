using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable(TableConsts.Users);
            builder.Entity<IdentityRole>().ToTable(TableConsts.Roles);
            builder.Entity<IdentityRoleClaim<string>>().ToTable(TableConsts.RoleClaims);
            builder.Entity<IdentityUserClaim<string>>().ToTable(TableConsts.UserClaims);
            builder.Entity<IdentityUserLogin<string>>().ToTable(TableConsts.UserLogins);
            builder.Entity<IdentityUserToken<string>>().ToTable(TableConsts.UserTokens);
            builder.Entity<IdentityUserRole<string>>().ToTable(TableConsts.UserRoles);
        }
    }

    public static class TableConsts
    {
        public const string Roles = "Roles";
        public const string RoleClaims = "RoleClaims";
        public const string Users = "Users";
        public const string UserClaims = "UserClaims";
        public const string UserLogins = "UserLogins";
        public const string UserTokens = "UserTokens";
        public const string UserRoles = "UserRoles";
    }
}
