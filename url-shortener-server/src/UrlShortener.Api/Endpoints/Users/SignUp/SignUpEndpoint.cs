using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.Users.Command.SignUp;

namespace UrlShortener.Api.Endpoints.Users.SignUp;

public class SignUpEndpoint:BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/signup", async (ISender sender, SignUpRequest request) =>
        {
            SignUpCommand commandRequest = new(request.Login, request.Password);

            var response = await sender.Send(commandRequest);
            return !response.IsSuccess ? HandleFailure(response) : Results.Ok();
        }).WithTags(EndpointTags.Users);
    }
}