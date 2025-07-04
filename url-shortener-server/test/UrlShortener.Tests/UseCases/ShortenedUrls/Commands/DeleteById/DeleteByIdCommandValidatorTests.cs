using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Tests.UseCases.ShortenedUrls.Commands.DeleteById;

public class DeleteByIdCommandValidatorTests
{
    private readonly DeleteByIdCommandValidator _validator = new();

    [Fact]
    public async Task Should_Have_Error_When_Id_Is_EmptyGuid()
    {
        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(new ShortenedUrlId(Guid.Empty),  new UserId(Guid.Empty), []);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be a valid identifier.");
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var command = new Application.UseCases.ShortenedUrls.Commands.DeleteById.DeleteByIdCommand(new ShortenedUrlId(Guid.NewGuid()),  new UserId(Guid.NewGuid()), []);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }
}