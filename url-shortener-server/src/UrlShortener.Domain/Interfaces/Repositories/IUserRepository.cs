using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User,UserId>
{
    Task<bool> ExistsByLoginAsync(string login);
}