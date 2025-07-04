using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

namespace UrlShortener.Application.Tests.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

public class RedirectToOriginalUrlQueryValidatorTests
{
    private readonly RedirectToOriginalUrlQueryValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_ShortCode_Is_Empty()
    {
        var query = new RedirectToOriginalUrlQuery("");

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.ShortCode)
            .WithErrorMessage("ShortCode must not be empty.");
    }

    [Theory]
    [InlineData("abcdefgh")]
    [InlineData("abcdef")]
    public void Should_Have_Error_When_ShortCode_Length_Is_Invalid(string shortCode)
    {
        var query = new RedirectToOriginalUrlQuery(shortCode);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.ShortCode)
            .WithErrorMessage("ShortCode must be exactly 7 characters long.");
    }

    [Theory]
    [InlineData("abcde!@")]
    [InlineData("abcd.f1")]
    [InlineData("123456$")]
    public void Should_Have_Error_When_ShortCode_Has_Invalid_Characters(string shortCode)
    {
        var query = new RedirectToOriginalUrlQuery(shortCode);

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.ShortCode)
            .WithErrorMessage("ShortCode must be a base62 string (A-Z, a-z, 0-9).");
    }

    [Theory]
    [InlineData("Abc1234")]
    [InlineData("A1B2C3D")]
    [InlineData("abcdefG")]
    public void Should_Not_Have_Error_When_ShortCode_Is_Valid(string shortCode)
    {
        var query = new RedirectToOriginalUrlQuery(shortCode);

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }
}