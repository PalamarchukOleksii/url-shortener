using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.Users.Command.SignIn;

namespace UrlShortener.Application.Tests.UseCases.Users.Commands.SignIn;

public class SignInCommandValidatorTests
{
    private readonly SignInCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Login_Is_Empty()
    {
        var command = new Application.UseCases.Users.Command.SignIn.SignInCommand("", "123456");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Login)
            .WithErrorMessage("Email must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var command = new Application.UseCases.Users.Command.SignIn.SignInCommand("user", "");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Password)
            .WithErrorMessage("Password must not be empty.");
    }
}