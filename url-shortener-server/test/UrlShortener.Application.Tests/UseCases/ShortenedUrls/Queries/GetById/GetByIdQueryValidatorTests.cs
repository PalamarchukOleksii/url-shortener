using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;
using UrlShortener.Domain.Models.ShortenedUrlModel;

namespace UrlShortener.Application.Tests.UseCases.ShortenedUrls.Queries.GetById;

public class GetByIdQueryValidatorTests
{
    private readonly GetByIdQueryValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var query = new GetByIdQuery(new ShortenedUrlId(Guid.Empty));

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(q => q.Id)
            .WithErrorMessage("Id must be a valid identifier.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var query = new GetByIdQuery(new ShortenedUrlId(Guid.NewGuid()));

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }
}