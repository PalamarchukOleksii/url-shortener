using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.Users.Command.SignIn;

namespace UrlShortener.Api.Endpoints.Users.SignIn;

public class SignInEndpoint :BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/signin", async (ISender sender, SignInRequest request) =>
        {
            SignInCommand commandRequest = new(request.Login, request.Password);

            var response = await sender.Send(commandRequest);
            return !response.IsSuccess ? HandleFailure(response) : Results.Ok();
        }).WithTags(EndpointTags.Users);
    }
}