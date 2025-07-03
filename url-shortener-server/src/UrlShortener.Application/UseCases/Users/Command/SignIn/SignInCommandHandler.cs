using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Users.Command.SignIn;

public class SignInCommandHandler(IUserRepository userRepository, IHasher hasher)
{
    public async Task<Result> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLoginAsync(command.Login);
        if (user is null)
            return Result.Failure(new Error(
                "User.NotFound",
                $"User with login {command.Login} was not found"));

        var isPasswordValid = await hasher.VerifyAsync(command.Password, user.HashedPassword);
        if (!isPasswordValid)
            return Result.Failure(new Error(
                "User.IncorrectPassword",
                "Incorrect password"));

        return Result.Success();
    }
}