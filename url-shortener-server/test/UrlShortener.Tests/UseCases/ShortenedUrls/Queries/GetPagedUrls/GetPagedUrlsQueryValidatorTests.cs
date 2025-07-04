using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;

namespace UrlShortener.Tests.UseCases.ShortenedUrls.Queries.GetPagedUrls;

public class GetPagedUrlsQueryValidatorTests
{
    private readonly GetPagedUrlsQueryValidator _validator = new();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Have_Error_When_Page_Is_Not_Greater_Than_Zero(int page)
    {
        var query = new GetPagedUrlsQuery(page, 10);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Page)
            .WithErrorMessage("Page number must be greater than 0.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Should_Have_Error_When_Size_Is_Not_Greater_Than_Zero(int size)
    {
        var query = new GetPagedUrlsQuery(1, size);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Size)
            .WithErrorMessage("Page size must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Size_Exceeds_Maximum()
    {
        var query = new GetPagedUrlsQuery(1, 101);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Size)
            .WithErrorMessage("Page size must not exceed 100.");
    }
}