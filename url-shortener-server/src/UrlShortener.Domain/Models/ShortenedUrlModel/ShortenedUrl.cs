using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.ShortenedUrlModel;

public class ShortenedUrl
{
    public ShortenedUrlId Id { get; init; }
    public string OriginalUrl { get; init; }
    public string ShortCode { get; init; }
    public UserId CreatorId { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public User Creator { get; init; }
}