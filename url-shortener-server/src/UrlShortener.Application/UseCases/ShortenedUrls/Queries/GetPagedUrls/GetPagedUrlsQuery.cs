using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;

public record GetPagedUrlsQuery(int Page, int Size) : IQuery<ICollection<ShortenedUrlDtoShort>>;