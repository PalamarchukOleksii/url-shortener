using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetAllShortenedUrls;

public record GetPagedUrlsQuery(int Page, int Size) : IQuery<ICollection<ShortenedUrlDtoShort>>;