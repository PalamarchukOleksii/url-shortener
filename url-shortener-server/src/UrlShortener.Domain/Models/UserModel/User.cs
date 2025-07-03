using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Domain.Models.UserModel;

public class User
{
    public UserId Id { get; init; }
    public string Login { get; init; }
    public string HashedPassword { get; init; }
    
    public ICollection<ShortenedUrl> UserUrls { get; init; }
    public ICollection<UserRole> UserRoles { get; init; }
}