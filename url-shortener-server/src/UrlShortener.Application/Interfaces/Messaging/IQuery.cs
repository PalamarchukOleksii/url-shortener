using MediatR;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}