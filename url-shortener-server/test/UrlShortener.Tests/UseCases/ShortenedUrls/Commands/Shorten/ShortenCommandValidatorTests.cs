using System;
using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.Shorten;
using UrlShortener.Domain.Models.UserModel;
using Xunit;

namespace UrlShortener.Tests.UseCases.ShortenedUrls.Commands.Shorten;

public class ShortenCommandValidatorTests
{
    private readonly ShortenCommandValidator _validator = new();
    
    [Fact]
    public void Should_Have_Error_When_OriginalUrl_Is_Empty()
    {
        var command = new ShortenCommand("", new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.OriginalUrl)
            .WithErrorMessage("Original URL must not be empty.");
    }

    [Theory]
    [InlineData("invalid-url")]
    [InlineData("ftp://notallowed.com")]
    [InlineData("www.google.com")]
    public void Should_Have_Error_When_OriginalUrl_Is_Invalid(string url)
    {
        var command = new ShortenCommand(url, new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.OriginalUrl)
            .WithErrorMessage("Original URL must be a valid URL.");
    }

    [Fact]
    public void Should_Have_Error_When_CallerId_Is_Empty()
    {
        var command = new ShortenCommand("https://example.com", new UserId(Guid.Empty));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.CallerId)
            .WithErrorMessage("CallerId must be a valid user id.");
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Command_Is_Valid()
    {
        var command = new ShortenCommand("https://example.com", new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
