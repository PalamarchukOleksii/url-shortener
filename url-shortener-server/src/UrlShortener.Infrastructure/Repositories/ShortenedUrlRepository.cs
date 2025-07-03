using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class ShortenedUrlRepository(ApplicationDbContext context) : IShortenedUrlRepository
{
    public async Task<ShortenedUrl?> GetByIdAsync(ShortenedUrlId id)
    {
        return await context.ShortenedUrls.FirstOrDefaultAsync(x => x.Id == id);
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
        var shortenedUrl = await context.ShortenedUrls.FirstOrDefaultAsync(x => x.Id == id);
        if (shortenedUrl != null) context.ShortenedUrls.Remove(shortenedUrl);
    }

    public async Task<bool> ExistsByShortCodeAsync(string shortCode)
    {
        return await context.ShortenedUrls.AnyAsync(su => su.ShortCode == shortCode);
    }

    public async Task<bool> ExistsByOriginalUrlAsync(string originalUrl)
    {
        return await context.ShortenedUrls.AnyAsync(su => su.OriginalUrl == originalUrl);
    }

    public async Task<bool> ExistByIdAsync(ShortenedUrlId id)
    {
        return await context.ShortenedUrls.AnyAsync(su => su.Id == id);
    }

    public async Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode)
    {
        return await context.ShortenedUrls.FirstOrDefaultAsync(su => su.ShortCode == shortCode);
    }

    public async Task<ICollection<ShortenedUrl>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await context.ShortenedUrls
            .AsNoTracking()
            .OrderByDescending(x => x.RedirectCount)
            .Skip(Math.Max(pageNumber - 1, 0) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}