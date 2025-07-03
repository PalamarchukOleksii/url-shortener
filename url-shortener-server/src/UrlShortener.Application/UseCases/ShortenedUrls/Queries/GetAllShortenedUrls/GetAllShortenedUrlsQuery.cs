using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetAllShortenedUrls;

public record GetAllShortenedUrlsQuery(int Page, int Size) : IQuery<ICollection<ShortenedUrlDto>>;