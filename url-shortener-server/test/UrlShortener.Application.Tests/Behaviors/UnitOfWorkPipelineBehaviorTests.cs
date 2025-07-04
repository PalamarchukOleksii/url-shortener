using MediatR;
using Moq;
using UrlShortener.Application.Behaviors;
using UrlShortener.Application.Interfaces.Data;

namespace UrlShortener.Application.Tests.Behaviors;

public class UnitOfWorkPipelineBehaviorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly UnitOfWorkPipelineBehavior<DummyRequest, DummyResponse> _behavior;

    public UnitOfWorkPipelineBehaviorTests()
    {
        _behavior = new UnitOfWorkPipelineBehavior<DummyRequest, DummyResponse>(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Call_SaveChanges_And_Return_Response()
    {
        var dummyRequest = new DummyRequest();
        var dummyResponse = new DummyResponse();

        var nextCalled = false;
        RequestHandlerDelegate<DummyResponse> next = (CancellationToken _) =>
        {
            nextCalled = true;
            return Task.FromResult(dummyResponse);
        };

        var response = await _behavior.Handle(dummyRequest, next, CancellationToken.None);

        Assert.True(nextCalled);
        Assert.Equal(dummyResponse, response);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private class DummyRequest { }
    private class DummyResponse { }
}