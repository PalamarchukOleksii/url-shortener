using UrlShortener.Application.Dtos;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetAllShortenedUrls;

public class GetAllShortenedUrlsQueryHandler(IShortenedUrlRepository  shortenedUrlRepository)
{
    public async Task<Result<ICollection<ShortenedUrlDto>>> Handle(GetAllShortenedUrlsQuery query, CancellationToken cancellationToken)
    {
        var urls = await shortenedUrlRepository.GetPagedAsync(query.Page, query.Size);

        return Result.Success<ICollection<ShortenedUrlDto>>(urls.Select(url => new ShortenedUrlDto { Id = url.Id, OriginalUrl = url.OriginalUrl, ShortCode = url.ShortCode, CreatorId = url.CreatorId }).ToList());
    }
}