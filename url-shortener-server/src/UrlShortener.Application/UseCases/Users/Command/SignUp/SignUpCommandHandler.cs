using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Users.Command.SignUp;

public class SignUpCommandHandler(
    IUserRepository userRepository,
    IHasher hasher,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository) : ICommandHandler<SignUpCommand>
{
    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByLoginAsync(command.Login))
            return Result.Failure(new Error(
                "User.NotUniqueLogin",
                $"User with login {command.Login} has already exist."));

        var passwordHash = await hasher.HashAsync(command.Password);
        if (passwordHash is null)
            return Result.Failure(new Error(
                "Hasher.Failed",
                "Unable to generate secure password hash."));

        var user = new User
        {
            Login = command.Login,
            HashedPassword = passwordHash
        };
        await userRepository.AddAsync(user);

        var userRole = await roleRepository.GetByNameAsync("User");
        if (userRole is null)
            return Result.Failure(new Error(
                "Role.MissingDefaultUserRole",
                "Default role 'User' not found."));

        var userRoleLink = new UserRole
        {
            UserId = user.Id,
            RoleId = userRole.Id
        };
        await userRoleRepository.AddAsync(userRoleLink);

        return Result.Success();
    }
}