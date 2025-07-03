using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.ShortenedUrlModel;

public class ShortenedUrl
{
    public ShortenedUrl(string originalUrl, string shortCode, UserId creatorId)
    {
        Id = new ShortenedUrlId(Guid.NewGuid());
        OriginalUrl = originalUrl;
        ShortCode = shortCode;
        CreatorId = creatorId;
        CreatedAt = DateTime.UtcNow;
    }

    public ShortenedUrlId Id { get; init; }
    public string OriginalUrl { get; init; }
    public string ShortCode { get; init; }
    public UserId CreatorId { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public virtual User Creator { get; init; }
}