using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.GetById;

public class GetByIdEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/shortenedurls/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetByIdQuery(new ShortenedUrlId(id));
                var result = await sender.Send(query);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.Ok(result.Value);
            })
            .WithTags(EndpointTags.ShortenedUrls)
            .RequireAuthorization("UserOrAdmin");
    }
}