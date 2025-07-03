using MediatR;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.Interfaces.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}