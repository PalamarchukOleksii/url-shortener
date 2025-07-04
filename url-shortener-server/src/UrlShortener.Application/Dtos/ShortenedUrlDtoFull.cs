using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Dtos;

public class ShortenedUrlDtoFull
{
    public ShortenedUrlId Id { get; init; } = new(Guid.NewGuid());
    public string OriginalUrl { get; init; } = string.Empty;
    public string ShortCode { get; init; } = string.Empty;
    public UserId CreatorId { get; init; } = new(Guid.Empty);
    public string CreatorLogin { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public int RedirectCount { get; set; }
}