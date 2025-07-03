using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;

public record GetByIdQuery(ShortenedUrlId Id) : IQuery<ShortenedUrlDtoFull>;