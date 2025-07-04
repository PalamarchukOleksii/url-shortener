using FluentValidation;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;

public class GetByLanguageCodeQueryValidator : AbstractValidator<GetByLanguageCodeQuery>
{
    public GetByLanguageCodeQueryValidator()
    {
        RuleFor(x => x.LanguageCode)
            .NotEmpty().WithMessage("LanguageCode must not be null.");
    }
}