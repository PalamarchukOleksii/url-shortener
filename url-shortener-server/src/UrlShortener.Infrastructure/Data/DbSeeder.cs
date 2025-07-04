using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;
using UrlShortener.Infrastructure.Options;

namespace UrlShortener.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IHasher>();
        var adminOptions = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserOptions>>().Value;

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
                Login = adminOptions.Login,
                HashedPassword = await hasher.HashAsync(adminOptions.Password) ??
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
                Description = "The URL shortener generates unique short codes using a random base62 encoding method. It creates short codes that are 7 characters long, each character chosen from a set of 62 possible characters consisting of uppercase letters (A-Z), lowercase letters (a-z), and digits (0-9).\n\nTo generate a short code, the system uses a cryptographically secure random number generator to produce random bytes. Each byte is then mapped to a character in the base62 alphabet by taking the remainder when divided by 62. This ensures that each character in the short code is randomly selected from the full alphabet.\n\nBecause the codes are generated randomly, there is a chance of collisions, where a generated short code already exists in the database. To prevent duplicates, the system attempts up to 10 times to create a unique code. After each attempt, it checks whether the generated code is already in use. If a unique code is found, it is returned immediately. If all attempts fail, the last generated code is returned, assuming that collisions are extremely rare.\n\nThis approach provides a very large namespace of possible codes — approximately 3.5 trillion unique combinations — which greatly reduces the likelihood of collisions. Using a secure random generator also makes the codes unpredictable and difficult to guess, enhancing security and privacy.\n\nThe random nature of the codes means there is no exposure of sequential information, unlike incremental ID methods, which can reveal how many URLs have been shortened.\n\nWhile the collision risk is low, it is important that the database lookup to check for existing codes is efficient to maintain fast performance. Indexing the short code field is recommended.\n\nOverall, this algorithm offers a simple, secure, and efficient way to generate unique short codes suitable for most URL shortening needs.",
                LastEditAt = DateTime.UtcNow
            };
            await db.Abouts.AddAsync(about);
        }

        await db.SaveChangesAsync();
    }
}