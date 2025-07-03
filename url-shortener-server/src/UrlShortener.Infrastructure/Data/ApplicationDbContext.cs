using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<UserRole> UserRoles { get; init; }
    public DbSet<ShortenedUrl> ShortenedUrls { get; init; }
    public DbSet<About> Abouts { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}