using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

public class RedirectToOriginalUrlQueryHandler(IShortenedUrlRepository  shortenedUrlRepository) : IQueryHandler<RedirectToOriginalUrlQuery, string>
{
    public async Task<Result<string>> Handle(RedirectToOriginalUrlQuery query, CancellationToken cancellationToken)
    {
        var shortenedUrl = await shortenedUrlRepository.GetByShortCodeAsync(query.ShortCode);
        if (shortenedUrl == null)
        {
            return Result.Failure<string>(new Error(
                "ShortenedUrl.NotFound",
                $"Redirect URL not found for short code '{query.ShortCode}'."));
        }
        
        shortenedUrl.RedirectCount += 1;
        shortenedUrlRepository.Update(shortenedUrl);
        
        return Result.Success(shortenedUrl.OriginalUrl);
    }
}