using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IShortenedUrlRepository :IRepository<ShortenedUrl,ShortenedUrlId>
{
    Task<bool> ExistsByShortCodeAsync(string shortCode);
    Task<bool> ExistsByOriginalUrlAsync(string originalUrl);
    Task<bool> ExistByIdAsync(ShortenedUrlId id);
    Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode);
    Task<ICollection<ShortenedUrl>> GetPagedAsync(int pageNumber, int pageSize);
}