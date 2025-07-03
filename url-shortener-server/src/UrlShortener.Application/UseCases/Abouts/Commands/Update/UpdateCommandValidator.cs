using FluentValidation;

namespace UrlShortener.Application.UseCases.Abouts.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.AboutId)
            .NotNull().WithMessage("AboutId must not be null.")
            .Must(id => id.Value != Guid.Empty).WithMessage("AboutId must be a valid identifier.");

        RuleFor(x => x.NewDescription)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(4000).WithMessage("Description cannot exceed 4000 characters.");

        RuleFor(x => x.CallerId)
            .NotNull().WithMessage("CallerId must not be null.")
            .Must(id => id.Value != Guid.Empty).WithMessage("CallerId must be a valid identifier.");
    }
}