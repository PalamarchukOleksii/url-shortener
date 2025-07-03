using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.Users.Command.SignIn;

public record SignInCommand(string Login, string Password) : ICommand<UserDto>;