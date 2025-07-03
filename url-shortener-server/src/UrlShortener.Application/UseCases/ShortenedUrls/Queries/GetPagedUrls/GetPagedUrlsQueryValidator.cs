using FluentValidation;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetPagedUrls;

public class GetPagedUrlsQueryValidator : AbstractValidator<GetPagedUrlsQuery>
{
    public GetPagedUrlsQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(q => q.Size)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");
    }
}