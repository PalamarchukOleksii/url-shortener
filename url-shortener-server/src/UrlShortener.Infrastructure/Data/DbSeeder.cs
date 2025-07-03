using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IHasher>();

        var adminRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole is null)
        {
            adminRole = new Role
            {
                Id = new RoleId(Guid.NewGuid()),
                Name = "Admin"
            };
            await db.Roles.AddAsync(adminRole);
        }

        var userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "User");
        if (userRole is null)
        {
            userRole = new Role
            {
                Id = new RoleId(Guid.NewGuid()),
                Name = "User"
            };
            await db.Roles.AddAsync(userRole);
        }

        if (!await db.Users.AnyAsync(u => u.Login == "admin"))
        {
            var adminUserId = new UserId(Guid.NewGuid());
            var adminUser = new User
            {
                Id = adminUserId,
                Login = "admin",
                HashedPassword = await hasher.HashAsync("Passw0rd.") ??
                                 throw new Exception("Failed to hash admin password")
            };

            var adminUserRole = new UserRole
            {
                Id = new UserRoleId(Guid.NewGuid()),
                UserId = adminUserId,
                RoleId = adminRole.Id
            };

            await db.Users.AddAsync(adminUser);
            await db.UserRoles.AddAsync(adminUserRole);
        }

        var about = await db.Abouts.FirstOrDefaultAsync(x => x.Language == LanguageCode.En);
        if (about == null)
        {
            about = new About
            {
                Id = new AboutId(Guid.NewGuid()),
                Description = "This URL shortener uses a base62 encoding of a unique ID to generate short codes.",
                LastEditAt = DateTime.UtcNow
            };
            await db.Abouts.AddAsync(about);
        }

        await db.SaveChangesAsync();
    }
}