using FluentValidation;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteById;

public class DeleteByIdCommandValidator : AbstractValidator<DeleteByIdCommand>
{
    public DeleteByIdCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotNull().WithMessage("Id must not be null.")
            .Must(id => id.Value != Guid.Empty).WithMessage("Id must be a valid identifier.");
    }
}