using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.Users.Command.SignUp;

public record SignUpCommand(string Login, string Password) : ICommand;