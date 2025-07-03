using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;

namespace UrlShortener.Api.Endpoints.Users.SignOut;

public class SignOutEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/signout", async (HttpContext http) =>
        {
            await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.Ok();
        }).WithTags(EndpointTags.Users);
    }
}