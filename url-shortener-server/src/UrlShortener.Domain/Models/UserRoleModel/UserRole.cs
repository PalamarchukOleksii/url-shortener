using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Models.UserRoleModel;

public class UserRole
{
    public UserRoleId Id { get; init; }
    public UserId UserId { get; init; }
    public RoleId RoleId { get; init; }
    
    public User User { get; init; }
    public Role Role { get; init; }
}