using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Abouts.Commands.Update;

public class UpdateCommandHandler(IAboutRepository aboutRepository, IUserRoleRepository userRoleRepository)
    : ICommandHandler<UpdateCommand>
{
    public async Task<Result> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var callerRoles = await userRoleRepository.GetByUserIdAsync(request.CallerId);
        if (!callerRoles.Any(role => string.Equals(role.Role.Name, "Admin", StringComparison.OrdinalIgnoreCase)))
            return Result.Failure(new Error(
                "Authorization.Failed",
                "Caller does not have admin permissions."));

        var about = await aboutRepository.GetByIdAsync(request.AboutId);
        if (about == null)
            return Result.Failure(new Error(
                "About.NotFound",
                $"About with ID '{request.AboutId}' not found."));

        about.Update(request.NewDescription);

        aboutRepository.Update(about);

        return Result.Success();
    }
}