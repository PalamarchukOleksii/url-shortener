using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.ShortenUrl;

public record ShortenUrlRequest(string OriginalUrl, UserId CallerId);