using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class AboutRepository(ApplicationDbContext context) : IAboutRepository
{
    public async Task<About?> GetByIdAsync(AboutId id)
    {
        return await context.Abouts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<About>> GetAllAsync()
    {
        return await context.Abouts.ToListAsync();
    }

    public async Task AddAsync(About entity)
    {
        await context.Abouts.AddAsync(entity);
    }

    public void Update(About entity)
    {
        context.Abouts.Update(entity);
    }

    public async Task DeleteAsync(AboutId id)
    {
        var entity = await context.Abouts.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            context.Abouts.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}