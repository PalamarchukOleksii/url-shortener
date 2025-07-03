using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.UseCases.Abouts.Commands.Update;

public record UpdateCommand(AboutId AboutId, string NewDescription, UserId CallerId) : ICommand;