using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos.UserDtos;
using UrlShortener.Application.UseCases.Users.Command.SignIn;
using UrlShortener.Application.UseCases.Users.Command.SignUp;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(SignInCommandHandler signInCommandHandler, SignUpCommandHandler signUpCommandHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await signUpCommandHandler.Handle(new SignUpCommand(request.Login, request.Password), cancellationToken);

        return response.IsSuccess ? Ok() : BadRequest(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await signInCommandHandler.Handle(new SignInCommand(request.Login, request.Password), cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}