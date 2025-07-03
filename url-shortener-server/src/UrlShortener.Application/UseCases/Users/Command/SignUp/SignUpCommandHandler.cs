using UrlShortener.Application.Interfaces.Data;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Users.Command.SignUp;

public class SignUpCommandHandler(IUserRepository userRepository, IHasher hasher, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByLoginAsync(command.Login))
            return Result.Failure(new Error(
                "User.NotUniqueLogin",
                $"User with login {command.Login} has already exist"));

        var passwordHash = await hasher.HashAsync(command.Password);
        if (passwordHash is null)
            return Result.Failure(new Error(
                "Hasher.Failed",
                "Unable to generate secure password hash"));
        
        var user = new User(command.Login, passwordHash);

        await userRepository.AddAsync(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}