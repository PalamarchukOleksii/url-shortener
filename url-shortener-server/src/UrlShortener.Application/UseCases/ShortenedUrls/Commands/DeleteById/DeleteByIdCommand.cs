using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;

public record DeleteByIdCommand(ShortenedUrlId Id) : ICommand;