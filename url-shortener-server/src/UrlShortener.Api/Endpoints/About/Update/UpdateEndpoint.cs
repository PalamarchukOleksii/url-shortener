using System.Security.Claims;
using Microsoft.AspNetCore.Routing;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using MediatR;
using UrlShortener.Application.UseCases.Abouts.Commands.Update;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Api.Endpoints.About.Update;

public class UpdateEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/about", async (UpdateRequest request, ISender sender, HttpContext http) =>
            {
                var userIdClaim = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdClaim is null)
                    return Results.Unauthorized();

                var callerId = new UserId(Guid.Parse(userIdClaim));
                
                var command = new UpdateCommand(
                    request.Id,
                    request.Description,
                    callerId);

                var result = await sender.Send(command);

                return result.IsFailure
                    ? HandleFailure(result)
                    : Results.Ok();
            })
            .WithTags(EndpointTags.About)
            .RequireAuthorization("AdminOnly");
    }
}