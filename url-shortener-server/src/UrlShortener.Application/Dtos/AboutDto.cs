using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Application.Dtos;

public class AboutDto
{
    public AboutId Id { get; init; } = new AboutId(Guid.NewGuid());
    public string Language { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime LastEditAt { get; init; } = DateTime.UtcNow;
}