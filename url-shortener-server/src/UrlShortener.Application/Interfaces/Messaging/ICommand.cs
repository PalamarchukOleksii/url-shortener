using MediatR;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}