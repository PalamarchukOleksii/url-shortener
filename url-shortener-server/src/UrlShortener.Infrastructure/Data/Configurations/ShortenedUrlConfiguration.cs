using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Models.ShortenedUrlModel;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Infrastructure.Data.Configurations;

public class ShortenedUrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
{
    public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new ShortenedUrlId(value))
            .ValueGeneratedNever();

        builder.Property(x => x.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(x => x.ShortCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CreatorId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.RedirectCount)
            .IsRequired();

        builder.HasIndex(x => x.ShortCode)
            .IsUnique();

        builder.HasIndex(x => x.OriginalUrl)
            .IsUnique();
    }
}