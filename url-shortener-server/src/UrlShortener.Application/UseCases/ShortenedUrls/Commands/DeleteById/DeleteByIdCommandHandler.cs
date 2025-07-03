using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;

public class DeleteByIdCommandHandler(
    IShortenedUrlRepository shortenedUrlRepository) : ICommandHandler<DeleteByIdCommand>
{
    public async Task<Result> Handle(DeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var shortenedUrl = await shortenedUrlRepository.GetByIdAsync(request.Id);
        if (shortenedUrl is null)
        {
            return Result.Failure(new Error(
                "ShortenedUrl.NotFound",
                $"Redirect URL not found for id '{request.Id.Value}'."));
        }

        var isOwner = shortenedUrl.CreatorId == request.CallerId;
        var isAdmin = request.Roles.Contains("Admin");

        if (!isOwner && !isAdmin)
        {
            return Result.Failure(new Error(
                "Authorization.Forbidden",
                "Only the owner or an admin can delete this shortened URL."));
        }

        await shortenedUrlRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}