using Moq;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.Shorten;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Tests.UseCases.ShortenedUrls.Commands.Shorten;

public class ShortenCommandHandlerTests
{
    private readonly Mock<IUrlShortenerService> _urlShortenerServiceMock = new();
    private readonly Mock<IShortenedUrlRepository> _shortenedUrlRepositoryMock = new();
    private readonly ShortenCommandHandler _handler;

    public ShortenCommandHandlerTests()
    {
        _handler = new ShortenCommandHandler(
            _urlShortenerServiceMock.Object,
            _shortenedUrlRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Url_Already_Exists()
    {
        var command = new ShortenCommand("https://existing.com",new UserId(Guid.NewGuid()));
        
        _shortenedUrlRepositoryMock.Setup(r => r.ExistsByOriginalUrlAsync(command.OriginalUrl))
            .ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("ShortenedUrl.UrlAlreadyExists", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Create_Short_Code_Fails()
    {
        var command = new ShortenCommand("https://example.com",new UserId(Guid.NewGuid()));

        _shortenedUrlRepositoryMock.Setup(r => r.ExistsByOriginalUrlAsync(command.OriginalUrl))
            .ReturnsAsync(false);

        _urlShortenerServiceMock.Setup(s => s.CreateShortCodeAsync())
            .ReturnsAsync((string?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("ShortenedUrl.FailedToCreateShortCode", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_Shortened_Url_Created()
    {
        var command = new ShortenCommand("https://newsite.com",new UserId(Guid.NewGuid()));

        _shortenedUrlRepositoryMock.Setup(r => r.ExistsByOriginalUrlAsync(command.OriginalUrl))
            .ReturnsAsync(false);

        _urlShortenerServiceMock.Setup(s => s.CreateShortCodeAsync())
            .ReturnsAsync("abc123");

        _shortenedUrlRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ShortenedUrl>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.OriginalUrl, result.Value.OriginalUrl);
        Assert.Equal("abc123", result.Value.ShortCode);
        Assert.Equal(command.CallerId, result.Value.CreatorId);
    }
}
