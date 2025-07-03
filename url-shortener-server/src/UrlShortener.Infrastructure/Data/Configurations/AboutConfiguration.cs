using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Infrastructure.Data.Configurations;

public class AboutConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .HasConversion( id => id.Value, value => new AboutId(value))
            .ValueGeneratedNever();
        
        builder.Property(a => a.Language)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(5);
        
        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(4000);
        
        builder.Property(a => a.LastEditAt)
            .IsRequired();
    }
}