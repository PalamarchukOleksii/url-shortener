using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos.UserDtos;
using UrlShortener.Application.UseCases.Users.Command.SignIn;
using UrlShortener.Application.UseCases.Users.Command.SignUp;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(SignInCommandHandler signInCommandHandler, SignUpCommandHandler signUpCommandHandler) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await signUpCommandHandler.Handle(new SignUpCommand(request.Login, request.Password), cancellationToken);

        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] SigninRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await signInCommandHandler.Handle(new SignInCommand(request.Login, request.Password), cancellationToken);

        return Ok();
    }
}