using Moq;
using UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Application.Tests.UseCases.About.Queries.GetByLanguageCode;

public class GetByLanguageCodeQueryHandlerTests
{
    private readonly Mock<IAboutRepository> _aboutRepository = new();
    private readonly GetByLanguageCodeQueryHandler _handler;

    public GetByLanguageCodeQueryHandlerTests()
    {
        _handler = new GetByLanguageCodeQueryHandler(_aboutRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Language_Code_Is_Invalid()
    {
        var query = new GetByLanguageCodeQuery("invalid");
        
        var result = await _handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsFailure);
        Assert.Equal("About.InvalidLanguageCode", result.Error?.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_About_Not_Found()
    {
        var query = new GetByLanguageCodeQuery("en");
        _aboutRepository
            .Setup(r => r.GetByLanguageCode(LanguageCode.En))
            .ReturnsAsync((Domain.Models.AboutModel.About)null!);
        
        var result = await _handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsFailure);
        Assert.Equal("About.NotFound", result.Error?.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAboutIsFound()
    {
        var query = new GetByLanguageCodeQuery("en");
        var about = new Domain.Models.AboutModel.About{
            Id = new AboutId(Guid.NewGuid()),
            Description = "About text",
            Language = LanguageCode.En,
            LastEditAt = DateTime.UtcNow,
        };
    
        _aboutRepository.Setup(r => r.GetByLanguageCode(LanguageCode.En)).ReturnsAsync(about);
        
        var result = await _handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(about.Description, result.Value.Description);
        Assert.Equal(about.Language.ToString(), result.Value.Language);
    }
}
