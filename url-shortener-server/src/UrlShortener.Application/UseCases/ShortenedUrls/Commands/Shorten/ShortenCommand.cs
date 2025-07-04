using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.Shorten;

public record ShortenCommand(string OriginalUrl, UserId CallerId) : ICommand<ShortenedUrlDtoShort>;