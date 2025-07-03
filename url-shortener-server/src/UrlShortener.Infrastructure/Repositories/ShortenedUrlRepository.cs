using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class ShortenedUrlRepository(ApplicationDbContext context): IShortenedUrlRepository
{
    public async Task<ShortenedUrl?> GetByIdAsync(ShortenedUrlId id)
    {
        return await context.ShortenedUrls.FindAsync(id);
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllAsync()
    {
        return await context.ShortenedUrls.ToListAsync();
    }

    public async Task AddAsync(ShortenedUrl entity)
    {
        await context.ShortenedUrls.AddAsync(entity);
    }

    public void Update(ShortenedUrl entity)
    {
        context.ShortenedUrls.Update(entity);
    }

    public async Task DeleteAsync(ShortenedUrlId id)
    {
        var shortenedUrl = await context.ShortenedUrls.FindAsync(id);
        if (shortenedUrl != null)
        {
            context.ShortenedUrls.Remove(shortenedUrl);
        }
    }

    public async Task<bool> ExistsByShortCodeAsync(string shortCode)
    {
        return await context.ShortenedUrls.AnyAsync(su => su.ShortCode == shortCode);
    }

    public async Task<bool> ExistsByOriginalUrlAsync(string originalUrl)
    {
        return await context.ShortenedUrls.AnyAsync(su => su.OriginalUrl == originalUrl);
    }
}