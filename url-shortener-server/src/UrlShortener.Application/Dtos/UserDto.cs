using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Application.Dtos;

public class UserDto
{
    public UserId Id { get; init; } = new UserId(Guid.NewGuid());
    public string Login { get; init; } = string.Empty;
    public ICollection<Role> Roles { get; init; } = new List<Role>();
}