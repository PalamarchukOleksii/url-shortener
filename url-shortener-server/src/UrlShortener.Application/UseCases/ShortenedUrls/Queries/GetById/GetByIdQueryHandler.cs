using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;

public class GetByIdQueryHandler(IShortenedUrlRepository shortenedUrlRepository) : IQueryHandler<GetByIdQuery, ShortenedUrlDtoFull>
{
    public async Task<Result<ShortenedUrlDtoFull>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var shortenedUrl = await shortenedUrlRepository.GetByIdAsync(request.Id);
        if (shortenedUrl is null)
        {
            return Result.Failure<ShortenedUrlDtoFull>(new Error(
                "ShortenedUrl.NotFound",
                $"Redirect URL not found for id '{request.Id.Value}'."));
        }

        var shortenedUrlDto = new ShortenedUrlDtoFull
        {
            Id = shortenedUrl.Id,
            OriginalUrl = shortenedUrl.OriginalUrl,
            ShortCode = shortenedUrl.ShortCode,
            CreatorId = shortenedUrl.CreatorId,
            CreatedAt = shortenedUrl.CreatedAt,
            RedirectCount = shortenedUrl.RedirectCount,
        };
        
        return Result.Success<ShortenedUrlDtoFull>(shortenedUrlDto);
    }
}