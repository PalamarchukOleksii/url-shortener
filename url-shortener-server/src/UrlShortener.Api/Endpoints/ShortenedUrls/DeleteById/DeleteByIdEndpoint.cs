using System.Security.Claims;
using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Api.Endpoints.ShortenedUrls.DeleteById;

public class DeleteByIdEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/shortenedurls/{id:guid}", async (
                Guid id,
                HttpContext http,
                ISender sender) =>
            {
                var userId = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null)
                    return Results.Unauthorized();

                var roleClaims = http.User.FindAll(ClaimTypes.Role);
                var roles = roleClaims.Select(rc => rc.Value).ToList();

                var command = new DeleteByIdCommand(
                    new ShortenedUrlId(id),
                    new UserId(Guid.Parse(userId)),
                    roles
                );

                var result = await sender.Send(command);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.NoContent();
            })
            .WithTags(EndpointTags.ShortenedUrls)
            .RequireAuthorization("UserOrAdmin");
    }
}