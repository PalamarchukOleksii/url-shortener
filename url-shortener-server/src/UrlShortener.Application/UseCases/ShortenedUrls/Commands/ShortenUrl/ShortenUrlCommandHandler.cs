using UrlShortener.Application.Interfaces.Data;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;

public class ShortenUrlCommandHandler(IUrlShortenerService urlShortenerService, IShortenedUrlRepository shortenedUrlRepository, IUnitOfWork unitOfWork)
{
    public async Task<Result<string>> Handle(ShortenUrlCommand command, CancellationToken cancellationToken)
    {
        if (await shortenedUrlRepository.ExistsByOriginalUrlAsync(command.OriginalUrl))
        {
            return Result.Failure<string>(new Error(
                "ShortenedUrl.UrlAlreadyExists",
                "The provided URL has already been shortened."));
        }

        var shortCode = await urlShortenerService.CreateShortCodeAsync();
        if (shortCode == null)
        {
            return Result.Failure<string>(new Error(
                "ShortenedUrl.FailedToCreateShortCode",
                "Failed to create shortCode"));
        }
        
        var shortenedUrl = new ShortenedUrl
        {
            OriginalUrl = command.OriginalUrl,
            ShortCode = shortCode,
            CreatorId = command.CallerId,
        };
        await  shortenedUrlRepository.AddAsync(shortenedUrl);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(shortCode);
    }
}