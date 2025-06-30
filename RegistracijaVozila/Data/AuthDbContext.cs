using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RegistracijaVozila.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "80e38d55-3872-4320-9f5b-54d5b2e50d38";
            var zaposleniRoleId = "49b719cf-9ebf-4151-af09-835f9c03f6b6";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Id = zaposleniRoleId,
                    Name = "Zaposleni",
                    NormalizedName = "Zaposleni".ToUpper(),
                    ConcurrencyStamp = zaposleniRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "93e88597-7567-4e0a-af32-d3c79597ab97";

            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "Danilo",
                Email = "jankovic.danilo23@gmail.com",
                NormalizedEmail = "jankovic.danilo23@gmail.com".ToUpper(),
                NormalizedUserName = "Danilo".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "tuv77tuv");

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId,
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = zaposleniRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
