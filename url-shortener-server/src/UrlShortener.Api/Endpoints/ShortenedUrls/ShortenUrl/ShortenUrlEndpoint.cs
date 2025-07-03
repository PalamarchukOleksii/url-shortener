using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.ShortenUrl;

public class ShortenUrlEndpoint: BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/shortened-urls/shorten-url", async (ISender sender, ShortenUrlRequest request) =>
        {
            var commandRequest = new ShortenUrlCommand(request.OriginalUrl, request.CallerId);
            var response = await sender.Send(commandRequest);

            return response.IsFailure ? HandleFailure(response) : Results.Redirect(response.Value);
        })
        .WithName(EndpointTags.ShortenedUrls);
    }
}