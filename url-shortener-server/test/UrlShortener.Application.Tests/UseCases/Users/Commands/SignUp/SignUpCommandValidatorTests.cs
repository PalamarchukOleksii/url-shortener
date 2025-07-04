using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.Users.Command.SignUp;

namespace UrlShortener.Application.Tests.UseCases.Users.Commands.SignUp;

public class SignUpCommandValidatorTests
{
    private readonly SignUpCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Login_Is_Empty()
    {
        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("","Valid123!");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Login)
            .WithErrorMessage("Login must not be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Login_Is_Too_Long()
    {
        var longLogin = new string('a', 51);
        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand(longLogin, "Valid123!");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Login)
            .WithErrorMessage("Login need to be shorter than 50 characters.");
    }

    [Theory]
    [InlineData(null, "Password must not be empty.")]
    [InlineData("", "Password must not be empty.")]
    [InlineData("short", "Password must be at least 8 characters long.")]
    [InlineData("alllowercase1!", "Password must contain at least one uppercase letter.")]
    [InlineData("ALLUPPERCASE1!", "Password must contain at least one lowercase letter.")]
    [InlineData("NoDigits!!", "Password must contain at least one digit.")]
    [InlineData("NoSpecialChar1", "Password must contain at least one special character.")]
    public void Should_Have_Errors_For_Invalid_Password(string password, string expectedError)
    {
        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("validuser", password);

        var result = _validator.TestValidate(command);

        Assert.Contains(result.Errors, e => e.ErrorMessage == expectedError);
    }
}
