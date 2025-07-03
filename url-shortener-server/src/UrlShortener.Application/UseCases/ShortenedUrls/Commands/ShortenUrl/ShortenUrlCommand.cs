using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;

public record ShortenUrlCommand(string OriginalUrl, UserId CallerId) : ICommand<string>;