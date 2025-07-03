using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Models.RoleModel;

namespace UrlShortener.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new RoleId(value))
            .ValueGeneratedNever();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Name)
            .IsUnique();
    }
}