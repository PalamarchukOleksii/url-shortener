using Moq;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Tests.UseCases.ShortenedUrls.Queries.GetPagedUrls;

public class GetPagedUrlsQueryHandlerTests
{
    private readonly Mock<IShortenedUrlRepository> _repositoryMock = new();
    private readonly GetPagedUrlsQueryHandler _handler;

    public GetPagedUrlsQueryHandlerTests()
    {
        _handler = new GetPagedUrlsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Empty_List_When_No_Urls()
    {
        var query = new GetPagedUrlsQuery(1, 10);

        _repositoryMock.Setup(r => r.GetPagedAsync(query.Page, query.Size))
            .ReturnsAsync(new List<ShortenedUrl>());

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task Handle_Should_Return_Correct_Data()
    {
        var urls = new List<ShortenedUrl>
        {
            new()
            {
                Id = new ShortenedUrlId(Guid.NewGuid()),
                OriginalUrl = "https://a.com",
                ShortCode = "a1",
                CreatorId = new UserId(Guid.NewGuid()),
            },
            new()
            {
                Id = new ShortenedUrlId(Guid.NewGuid()),
                OriginalUrl = "https://b.com",
                ShortCode = "b2",
                CreatorId = new UserId(Guid.NewGuid()),
            }
        };

        var query = new GetPagedUrlsQuery(1, 5);

        _repositoryMock.Setup(r => r.GetPagedAsync(query.Page, query.Size))
            .ReturnsAsync(urls);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(urls.Count, result.Value.Count);
        Assert.Equal(urls[0].OriginalUrl, result.Value.First().OriginalUrl);
    }
}
