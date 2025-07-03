using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.UrlShortening;

public record ShortenUrlCommand(string OriginalUrl, UserId CallerId);