using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IUserRoleRepository : IRepository<UserRole, UserRoleId>
{
    Task<ICollection<UserRole>> GetByUserIdAsync(UserId userId);
}