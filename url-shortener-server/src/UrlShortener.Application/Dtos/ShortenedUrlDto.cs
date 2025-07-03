using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Dtos;

public class ShortenedUrlDto
{
    public ShortenedUrlId Id { get; init; } = new ShortenedUrlId(Guid.NewGuid());
    public string OriginalUrl { get; init; } = string.Empty;
    public string ShortCode { get; init; } = string.Empty;
    public UserId CreatorId { get; init; } = new UserId(Guid.Empty);
}