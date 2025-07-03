using FluentValidation;
using UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetAboutByLanguageCode;

public class GetByLanguageCodeQueryValidator : AbstractValidator<GetByLanguageCodeQuery>
{
    public GetByLanguageCodeQueryValidator()
    {
        RuleFor(x => x.LanguageCode)
            .NotEmpty().WithMessage("LanguageCode must not be null.");
    }
}