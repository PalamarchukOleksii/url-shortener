using FluentValidation;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetById;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotNull().WithMessage("Id must not be null.")
            .Must(id => id.Value != Guid.Empty).WithMessage("Id must be a valid identifier.");
    }
}