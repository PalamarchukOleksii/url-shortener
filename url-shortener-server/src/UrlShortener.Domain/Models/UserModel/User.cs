using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Domain.Models.UserModel;

public class User
{
    public User(string login, string hashedPassword)
    {
        Login = login;
        HashedPassword = hashedPassword;
    }

    public UserId Id { get; init; }
    public string Login { get; init; }
    public string HashedPassword { get; init; }
    
    public virtual ICollection<ShortenedUrl> UserUrls { get; init; }
    public virtual ICollection<UserRole> UserRoles { get; init; }
}