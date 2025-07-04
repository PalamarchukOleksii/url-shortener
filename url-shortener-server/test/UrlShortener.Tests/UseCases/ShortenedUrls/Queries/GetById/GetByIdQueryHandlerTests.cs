using Moq;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Tests.UseCases.ShortenedUrls.Queries.GetById;

public class GetByIdQueryHandlerTests
{
    private readonly Mock<IShortenedUrlRepository> _repositoryMock = new();
    private readonly GetByIdQueryHandler _handler;

    public GetByIdQueryHandlerTests()
    {
        _handler = new GetByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Not_Found()
    {
        var id = new ShortenedUrlId(Guid.NewGuid());
        var query = new GetByIdQuery(id);

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((ShortenedUrl?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("ShortenedUrl.NotFound", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_Entity_Found()
    {
        var id = new ShortenedUrlId(Guid.NewGuid());
        var userId = new UserId(Guid.NewGuid());
        var query = new GetByIdQuery(id);

        var shortenedUrl = new ShortenedUrl
        {
            Id = id,
            OriginalUrl = "https://example.com",
            ShortCode = "abc123",
            CreatorId = userId,
            CreatedAt = DateTime.UtcNow,
            RedirectCount = 10,
            Creator = new User
            {
                Id = userId,
                Login = "testuser"
            }
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(shortenedUrl);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(shortenedUrl.Id, result.Value.Id);
        Assert.Equal(shortenedUrl.OriginalUrl, result.Value.OriginalUrl);
        Assert.Equal(shortenedUrl.ShortCode, result.Value.ShortCode);
        Assert.Equal(shortenedUrl.CreatorId, result.Value.CreatorId);
        Assert.Equal(shortenedUrl.Creator.Login, result.Value.CreatorLogin);
        Assert.Equal(shortenedUrl.CreatedAt, result.Value.CreatedAt);
        Assert.Equal(shortenedUrl.RedirectCount, result.Value.RedirectCount);
    }
}
