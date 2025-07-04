using Moq;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Tests.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

public class RedirectToOriginalUrlQueryHandlerTests
{
    private readonly RedirectToOriginalUrlQueryHandler _handler;
    private readonly Mock<IShortenedUrlRepository> _repositoryMock = new();

    public RedirectToOriginalUrlQueryHandlerTests()
    {
        _handler = new RedirectToOriginalUrlQueryHandler(_repositoryMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_Return_Failure_When_Not_Found()
    {
        const string code = "1234567";
        var query = new RedirectToOriginalUrlQuery(code);

        _repositoryMock.Setup(r => r.GetByShortCodeAsync(code)).ReturnsAsync((ShortenedUrl?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("ShortenedUrl.NotFound", result.Error.Code);
    }
    
    [Fact]
    public async Task Handle_Should_Return_Success_And_Update_RedirectCount()
    {
        var url = new ShortenedUrl
        {
            OriginalUrl = "https://example.com",
            ShortCode = "abc123",
            RedirectCount = 3
        };

        var query = new RedirectToOriginalUrlQuery("abc123");

        _repositoryMock.Setup(r => r.GetByShortCodeAsync(query.ShortCode))
            .ReturnsAsync(url);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("https://example.com", result.Value);
        Assert.Equal(4, url.RedirectCount);
        _repositoryMock.Verify(r => r.Update(url), Times.Once);
    }
}