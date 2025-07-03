using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(UserId id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task AddAsync(User entity)
    {
        await context.Users.AddAsync(entity);
    }

    public void Update(User entity)
    {
        context.Users.Update(entity);
    }

    public async Task DeleteAsync(UserId id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id.Value == id.Value);
        if (user != null)
        {
            context.Users.Remove(user);
        }
    }

    public async Task<bool> ExistsByLoginAsync(string login)
    {
        return await context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}