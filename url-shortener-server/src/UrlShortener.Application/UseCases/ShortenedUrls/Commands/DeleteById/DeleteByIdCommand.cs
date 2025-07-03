using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;

public record DeleteByIdCommand(ShortenedUrlId Id, UserId CallerId, IReadOnlyList<string> Roles) : ICommand;