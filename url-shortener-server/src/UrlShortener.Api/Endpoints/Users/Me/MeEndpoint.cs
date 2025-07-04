using System.Security.Claims;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;

namespace UrlShortener.Api.Endpoints.Users.Me;

public class MeEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/me", (HttpContext http) =>
            {
                var user = http.User;

                if (user.Identity is not { IsAuthenticated: true })
                    return Results.Unauthorized();

                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = user.FindFirst(ClaimTypes.Name)?.Value;
                var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

                if (userId == null)
                    return Results.BadRequest(new { Error = "User ID claim is missing." });

                var result = new
                {
                    Id = userId,
                    Username = userName,
                    Roles = roles
                };

                return Results.Ok(result);
            })
            .WithTags(EndpointTags.Users)
            .RequireAuthorization("UserOrAdmin");
    }
}