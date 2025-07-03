using FluentValidation;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

public class RedirectToOriginalUrlQueryValidator : AbstractValidator<RedirectToOriginalUrlQuery>
{
    public RedirectToOriginalUrlQueryValidator()
    {
        RuleFor(q => q.ShortCode)
            .NotEmpty().WithMessage("ShortCode must not be empty.")
            .Length(7).WithMessage("ShortCode must be exactly 7 characters long.")
            .Matches("^[A-Za-z0-9]{7}$").WithMessage("ShortCode must be a base62 string (A-Z, a-z, 0-9).");
    }
}