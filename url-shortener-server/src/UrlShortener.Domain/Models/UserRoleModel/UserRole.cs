using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.UserRoleModel;

public class UserRole
{
    public UserRoleId Id { get; init; } = new(Guid.NewGuid());
    public UserId UserId { get; init; } = new(Guid.Empty);
    public RoleId RoleId { get; init; } = new(Guid.Empty);

    public User User { get; init; } = null!;
    public Role Role { get; init; } = null!;
}