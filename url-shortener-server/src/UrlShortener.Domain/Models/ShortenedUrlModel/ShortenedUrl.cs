using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.ShortenedUrlModel;

public class ShortenedUrl
{
    public ShortenedUrlId Id { get; init; } = new(Guid.NewGuid());
    public string OriginalUrl { get; init; } = string.Empty;
    public string ShortCode { get; init; } = string.Empty;
    public UserId CreatorId { get; init; } = new(Guid.Empty);
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public int RedirectCount { get; set; }

    public virtual User Creator { get; init; } = null!;
}