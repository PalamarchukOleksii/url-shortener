using Moq;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Tests.UseCases.ShortenedUrls.Commands.DeleteById;

public class DeleteByIdCommandHandlerTests
{
    private readonly DeleteByIdCommandHandler _handler;
    private readonly Mock<IShortenedUrlRepository> _shortenedUrlRepository = new();

    public DeleteByIdCommandHandlerTests()
    {
        _handler = new DeleteByIdCommandHandler(_shortenedUrlRepository.Object);
    }
    
    [Fact]
    public async Task Handle_Should_Return_Failure_When_ShortenedUrl_NotFound()
    {
        var id = new ShortenedUrlId(Guid.NewGuid());

        _shortenedUrlRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((ShortenedUrl?)null);

        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(id, new UserId(Guid.NewGuid()), ["Admin"]);

        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsFailure);
        Assert.Equal("ShortenedUrl.NotFound", result.Error.Code);
    }
    
    [Fact]
    public async Task Handle_Should_Return_Failure_When_User_Not_Owner_Or_Admin()
    {
        
        var id = new ShortenedUrlId(Guid.NewGuid());
        var callerId = new UserId(Guid.NewGuid());

        var shortenedUrl = new ShortenedUrl
        {
            Id = id,
            CreatorId = new UserId(Guid.NewGuid())
        };

        _shortenedUrlRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(shortenedUrl);

        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(id, callerId, ["User"]);
        
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        
        Assert.True(result.IsFailure);
        Assert.Equal("Authorization.Forbidden", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_User_Is_Owner()
    {
        
        var id = new ShortenedUrlId(Guid.NewGuid());
        var callerId = new UserId(Guid.NewGuid());

        var shortenedUrl = new ShortenedUrl
        {
            Id = id,
            CreatorId = callerId
        };

        _shortenedUrlRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(shortenedUrl);

        _shortenedUrlRepository.Setup(r => r.DeleteAsync(id))
            .Returns(Task.CompletedTask);

        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(id, callerId, new List<string>());

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsSuccess);
        _shortenedUrlRepository.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_User_Is_Admin()
    {
        
        var id = new ShortenedUrlId(Guid.NewGuid());
        var callerId = new UserId(Guid.NewGuid());

        var shortenedUrl = new ShortenedUrl
        {
            Id = id,
            CreatorId = new UserId(Guid.NewGuid())
        };

        _shortenedUrlRepository.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(shortenedUrl);

        _shortenedUrlRepository.Setup(r => r.DeleteAsync(id))
            .Returns(Task.CompletedTask);

        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(id, callerId, ["Admin"]);

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsSuccess);
        _shortenedUrlRepository.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}