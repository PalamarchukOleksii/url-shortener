using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.UrlShortening;

public record UrlShorteningCommand(string OriginalUrl, UserId CallerId);