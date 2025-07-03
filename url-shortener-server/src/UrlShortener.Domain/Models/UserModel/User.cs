using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Domain.Models.UserModel;

public class User
{
    public UserId Id { get; init; } = new UserId(Guid.NewGuid());
    public string Login { get; init; } = string.Empty;
    public string HashedPassword { get; init; } =  string.Empty;
    
    public virtual ICollection<ShortenedUrl> UserUrls { get; init; } = new List<ShortenedUrl>();
    public virtual ICollection<UserRole> UserRoles { get; init; } = new List<UserRole>();
}