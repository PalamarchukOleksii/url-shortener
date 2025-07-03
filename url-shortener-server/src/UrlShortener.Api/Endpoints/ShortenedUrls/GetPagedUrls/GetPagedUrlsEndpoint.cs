using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.GetPagedUrls;

public class GetPagedUrlsEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/shortenedurls", async (int page, int count, ISender sender) =>
            {
                var query = new GetPagedUrlsQuery(page, count);
                var response = await sender.Send(query);

                return response.IsFailure ? HandleFailure(response) : Results.Ok(response.Value);
            })
            .WithTags(EndpointTags.ShortenedUrls).AllowAnonymous();
        ;
    }
}