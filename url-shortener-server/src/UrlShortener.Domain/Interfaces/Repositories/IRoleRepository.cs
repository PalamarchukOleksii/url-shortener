using UrlShortener.Domain.Models.RoleModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IRoleRepository: IRepository<Role,RoleId>
{
    Task<Role?> GetByNameAsync(string name);
}