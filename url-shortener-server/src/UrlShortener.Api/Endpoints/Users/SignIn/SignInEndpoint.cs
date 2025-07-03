using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.Users.Command.SignIn;

namespace UrlShortener.Api.Endpoints.Users.SignIn;

public class SignInEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/signin", async (HttpContext http, ISender sender, SignInRequest request) =>
        {
            if (http.User?.Identity?.IsAuthenticated == true)
                return Results.BadRequest(new { Error = "You are already signed in." });

            var commandRequest = new SignInCommand(request.Login, request.Password);

            var response = await sender.Send(commandRequest);

            if (!response.IsSuccess)
                return HandleFailure(response);

            var user = response.Value;

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.NameIdentifier, user.Id.Value.ToString())
            };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Results.Ok();
        }).WithTags(EndpointTags.Users).AllowAnonymous();
    }
}