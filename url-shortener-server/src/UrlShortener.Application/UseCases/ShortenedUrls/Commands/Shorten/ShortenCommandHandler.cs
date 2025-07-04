using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.Shorten;

public class ShortenCommandHandler(
    IUrlShortenerService urlShortenerService,
    IShortenedUrlRepository shortenedUrlRepository) : ICommandHandler<ShortenCommand, ShortenedUrlDtoShort>
{
    public async Task<Result<ShortenedUrlDtoShort>> Handle(ShortenCommand command, CancellationToken cancellationToken)
    {
        if (await shortenedUrlRepository.ExistsByOriginalUrlAsync(command.OriginalUrl))
            return Result.Failure<ShortenedUrlDtoShort>(new Error(
                "ShortenedUrl.UrlAlreadyExists",
                "The provided URL has already been shortened."));

        var shortCode = await urlShortenerService.CreateShortCodeAsync();
        if (shortCode == null)
            return Result.Failure<ShortenedUrlDtoShort>(new Error(
                "ShortenedUrl.FailedToCreateShortCode",
                "Failed to create shortCode."));

        var shortenedUrl = new ShortenedUrl
        {
            OriginalUrl = command.OriginalUrl,
            ShortCode = shortCode,
            CreatorId = command.CallerId
        };
        await shortenedUrlRepository.AddAsync(shortenedUrl);

        var shortenedUrlDto = new ShortenedUrlDtoShort
        {
            Id = shortenedUrl.Id,
            OriginalUrl = shortenedUrl.OriginalUrl,
            ShortCode = shortenedUrl.ShortCode,
            CreatorId = shortenedUrl.CreatorId
        };

        return Result.Success(shortenedUrlDto);
    }
}