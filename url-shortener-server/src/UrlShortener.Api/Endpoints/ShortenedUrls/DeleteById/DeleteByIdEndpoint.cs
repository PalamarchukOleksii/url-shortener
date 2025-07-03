using MediatR;
using Microsoft.AspNetCore.Routing;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.DeleteById;

public class DeleteByIdEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/shortenedurls/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteByIdCommand(new ShortenedUrlId(id));
                var result = await sender.Send(command);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.NoContent();
            })
            .WithTags(EndpointTags.ShortenedUrls);
    }
}