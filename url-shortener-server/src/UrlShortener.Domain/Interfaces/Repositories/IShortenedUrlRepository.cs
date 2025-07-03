using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IShortenedUrlRepository :IRepository<ShortenedUrl,ShortenedUrlId>
{
    Task<bool> ExistsByShortCodeAsync(string shortCode);
}