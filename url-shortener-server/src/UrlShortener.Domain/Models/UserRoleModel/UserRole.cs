using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.UserRoleModel;

public class UserRole
{
    public UserRoleId Id { get; init; } = new UserRoleId(Guid.NewGuid());
    public UserId UserId { get; init; } = new UserId(Guid.Empty);
    public RoleId RoleId { get; init; } = new RoleId(Guid.Empty);
    
    public User User { get; init; } = null!;
    public Role Role { get; init; } = null!;
}