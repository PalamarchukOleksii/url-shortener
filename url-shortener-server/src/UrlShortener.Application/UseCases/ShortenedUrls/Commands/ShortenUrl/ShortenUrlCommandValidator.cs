using FluentValidation;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;

public class ShortenUrlCommandValidator : AbstractValidator<ShortenUrlCommand>
{
    public ShortenUrlCommandValidator()
    {
        RuleFor(cmd => cmd.OriginalUrl)
            .NotEmpty().WithMessage("Original URL must not be empty.")
            .Must(IsValidUrl).WithMessage("Original URL must be a valid URL.");

        RuleFor(cmd => cmd.CallerId)
            .NotNull().WithMessage("CallerId must not be null.")
            .Must(id => id.Value != Guid.Empty).WithMessage("CallerId must be a valid user id.");
    }

    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}