using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Infrastructure.Data.Configurations;

public class UserConfiguration :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value))
            .ValueGeneratedNever();
        
        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.HashedPassword)
            .IsRequired();
        
        builder.HasMany(u => u.UserUrls)
            .WithOne(su => su.Creator)
            .HasForeignKey(su => su.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.Login)
            .IsUnique();
    }
}