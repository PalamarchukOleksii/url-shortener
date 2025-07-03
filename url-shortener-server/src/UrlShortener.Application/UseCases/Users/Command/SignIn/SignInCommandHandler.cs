using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Users.Command.SignIn;

public class SignInCommandHandler(IUserRepository userRepository, IHasher hasher, IUserRoleRepository userRoleRepository) : ICommandHandler<SignInCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLoginAsync(command.Login);
        if (user is null)
            return Result.Failure<UserDto>(new Error(
                "User.NotFound",
                $"User with login {command.Login} was not found."));

        var isPasswordValid = await hasher.VerifyAsync(command.Password, user.HashedPassword);
        if (!isPasswordValid)
            return Result.Failure<UserDto>(new Error(
                "User.IncorrectPassword",
                "Incorrect password."));
        
        var userRoles = await userRoleRepository.GetByUserIdAsync(user.Id);
        var roles = userRoles.Select(ur => ur.Role).ToList();

        var userDto = new UserDto
        {
            Id = user.Id,
            Login = user.Login,
            Roles = roles
        };

        return Result.Success(userDto);
    }
}