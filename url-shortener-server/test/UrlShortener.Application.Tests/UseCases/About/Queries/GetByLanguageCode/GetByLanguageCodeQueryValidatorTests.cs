using System.Threading.Tasks;
using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;
using Xunit;

namespace UrlShortener.Application.Tests.UseCases.About.Queries.GetByLanguageCode;

public class GetByLanguageCodeQueryValidatorTests
{
    private readonly GetByLanguageCodeQueryValidator _validator = new();

    [Fact]
    public async Task Should_Have_Error_When_LanguageCode_Is_Null_Or_Empty()
    {
        var query = new GetByLanguageCodeQuery("");
        
        var result = await _validator.TestValidateAsync(query);
        
        result.ShouldHaveValidationErrorFor(x => x.LanguageCode)
            .WithErrorMessage("LanguageCode must not be null.");
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_LanguageCode_Is_Provided()
    {
        var query = new GetByLanguageCodeQuery("EN");
        
        var result = await _validator.TestValidateAsync(query);
        
        result.ShouldNotHaveValidationErrorFor(x => x.LanguageCode);
    }
}