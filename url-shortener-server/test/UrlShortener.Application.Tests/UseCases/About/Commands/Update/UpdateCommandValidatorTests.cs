using FluentValidation.TestHelper;
using UrlShortener.Application.UseCases.Abouts.Commands.Update;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Tests.UseCases.About.Commands.Update;

public class UpdateCommandValidatorTests
{
    private readonly UpdateCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_AboutId_Is_Empty()
    {
        var command = new UpdateCommand(new AboutId(Guid.Empty),"Valid description", new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.AboutId)
              .WithErrorMessage("AboutId must be a valid identifier.");
    }

    [Fact]
    public void Should_Have_Error_When_CallerId_Is_Empty()
    {
        var command = new UpdateCommand(new AboutId(Guid.NewGuid()),"Valid description", new UserId(Guid.Empty));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.CallerId)
              .WithErrorMessage("CallerId must be a valid identifier.");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        var command = new UpdateCommand(new AboutId(Guid.NewGuid()),"", new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.NewDescription)
              .WithErrorMessage("Description cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Too_Long()
    {
        var command = new UpdateCommand(new AboutId(Guid.NewGuid()),new string('a', 4001), new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.NewDescription)
              .WithErrorMessage("Description cannot exceed 4000 characters.");
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var command = new UpdateCommand(new AboutId(Guid.NewGuid()),"Valid description", new UserId(Guid.NewGuid()));

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
