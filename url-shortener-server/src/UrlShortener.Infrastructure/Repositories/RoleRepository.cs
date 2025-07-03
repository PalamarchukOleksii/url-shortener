using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class RoleRepository(ApplicationDbContext context) :IRoleRepository
{
    public async Task<Role?> GetByIdAsync(RoleId id)
    {
        return await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await context.Roles.ToListAsync();
    }

    public async Task AddAsync(Role entity)
    {
        await context.Roles.AddAsync(entity);
    }

    public void Update(Role entity)
    {
        context.Roles.Update(entity);
    }

    public async Task DeleteAsync(RoleId id)
    {
        var role = await context.Roles.FirstOrDefaultAsync(u => u.Id.Value == id.Value);
        if (role != null)
        {
            context.Roles.Remove(role);
        }
    }
}