using FluentValidation;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetAboutByLanguageCode;

public class GetAboutByLanguageCodeQueryValidator :  AbstractValidator<GetAboutByLanguageCodeQuery>
{
    public GetAboutByLanguageCodeQueryValidator()
    {
        RuleFor(x => x.LanguageCode)
            .NotEmpty().WithMessage("LanguageCode must not be null.");
    }
}