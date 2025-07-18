using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Domain.Models.RoleModel;

public class Role
{
    public RoleId Id { get; init; } = new(Guid.NewGuid());
    public string Name { get; init; } = string.Empty;

    public ICollection<UserRole> RoleUsers { get; init; } = new List<UserRole>();
}