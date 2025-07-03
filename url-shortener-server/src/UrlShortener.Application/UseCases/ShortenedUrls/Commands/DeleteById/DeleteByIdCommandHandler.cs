using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;

public class DeleteByIdCommandHandler(IShortenedUrlRepository shortenedUrlRepository) :ICommandHandler<DeleteByIdCommand>
{
    public async Task<Result> Handle(DeleteByIdCommand request, CancellationToken cancellationToken)
    {
        if (!await shortenedUrlRepository.ExistByIdAsync(request.Id))
        {
            return Result.Failure(new Error(
                "ShortenedUrl.NotFound",
                $"Redirect URL not found for id '{request.Id.Value}'."));
        }

        await shortenedUrlRepository.DeleteAsync(request.Id);
        
        return Result.Success();
    }
}