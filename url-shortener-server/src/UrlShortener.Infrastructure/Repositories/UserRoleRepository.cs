using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.UserRoleModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext context) :IUserRoleRepository
{
    public async Task<UserRole?> GetByIdAsync(UserRoleId id)
    {
        return await context.UserRoles.FindAsync(id);
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await context.UserRoles.ToListAsync();
    }

    public async Task AddAsync(UserRole entity)
    {
        await context.UserRoles.AddAsync(entity);
    }

    public void Update(UserRole entity)
    {
        context.UserRoles.Update(entity);
    }

    public async Task DeleteAsync(UserRoleId id)
    {
        var userRole = await context.UserRoles.FindAsync(id);
        if (userRole != null)
        {
            context.UserRoles.Remove(userRole);
        }
    }
}