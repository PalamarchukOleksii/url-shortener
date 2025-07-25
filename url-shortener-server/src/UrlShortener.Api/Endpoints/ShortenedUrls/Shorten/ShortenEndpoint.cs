using System.Security.Claims;
using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.Shorten;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.Shorten;

public class ShortenEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/shortenedurls/shorten", async (
                HttpContext http,
                ISender sender,
                ShortenRequest request) =>
            {
                var userIdClaim = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdClaim is null)
                    return Results.Unauthorized();

                var userId = new UserId(Guid.Parse(userIdClaim));

                var commandRequest = new ShortenCommand(request.OriginalUrl, userId);
                var response = await sender.Send(commandRequest);

                return response.IsFailure
                    ? HandleFailure(response)
                    : Results.Ok(response.Value);
            })
            .WithTags(EndpointTags.ShortenedUrls)
            .RequireAuthorization("UserOrAdmin");
    }
}