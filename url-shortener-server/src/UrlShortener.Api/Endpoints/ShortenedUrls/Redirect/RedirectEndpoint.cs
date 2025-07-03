using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.Redirect;

public class RedirectEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/{shortCode}", async (ISender sender, string shortCode) =>
        {
            RedirectToOriginalUrlQuery queryRequest = new(shortCode);
            var response = await sender.Send(queryRequest);

            return response.IsFailure ? HandleFailure(response) : Results.Redirect(response.Value);
        });
    }
}