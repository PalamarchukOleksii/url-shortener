using FluentValidation;

namespace UrlShortener.Application.UseCases.Users.Command.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty().WithMessage("Email must not be empty.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password must not be empty.");
    }
}