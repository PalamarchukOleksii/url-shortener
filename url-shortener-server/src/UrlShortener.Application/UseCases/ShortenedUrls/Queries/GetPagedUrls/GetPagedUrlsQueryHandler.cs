using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;

public class GetPagedUrlsQueryHandler(IShortenedUrlRepository  shortenedUrlRepository) : IQueryHandler<GetPagedUrlsQuery, ICollection<ShortenedUrlDtoShort>>
{
    public async Task<Result<ICollection<ShortenedUrlDtoShort>>> Handle(GetPagedUrlsQuery query, CancellationToken cancellationToken)
    {
        var urls = await shortenedUrlRepository.GetPagedAsync(query.Page, query.Size);

        return Result.Success<ICollection<ShortenedUrlDtoShort>>(urls.Select(url => new ShortenedUrlDtoShort { Id = url.Id, OriginalUrl = url.OriginalUrl, ShortCode = url.ShortCode, CreatorId = url.CreatorId }).ToList());
    }
}