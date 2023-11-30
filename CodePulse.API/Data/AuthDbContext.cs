using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ViewPoint.API.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "03325d8b-c66f-4100-9e1f-33309c3a8953";
            var writerRoleId = "90d29ba4-ef25-4b6e-9abd-b8278382a8ca";

            //Create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //seed the values in the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create default user admin
            var adminUserId = "5ed54c3d-3efb-44b9-89c7-dbe17041ca54";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "patankermangesh@gmail.com",
                Email = "patankermangesh@gmail.com",
                NormalizedEmail = "patankermangesh@gmail.com".ToUpper(),
                NormalizedUserName = "patankermangesh@gmail.com".ToUpper()

            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Sushila@31");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to the admin user
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },
            };

            //seed the roles
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }




    }
}
