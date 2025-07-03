using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new UserRoleId(value))
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .ValueGeneratedNever();

        builder.Property(x => x.RoleId)
            .HasConversion(id => id.Value, value => new RoleId(value))
            .ValueGeneratedNever();
        
        builder.HasOne(x => x.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.RoleUsers)
            .HasForeignKey(x => x.RoleId);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RoleId);
    }
}