using System.Transactions;
using MediatR;
using UrlShortener.Application.Interfaces.Data;

namespace UrlShortener.Application.Behaviors;

public class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var response = await next(cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        transactionScope.Complete();

        return response;
    }
}