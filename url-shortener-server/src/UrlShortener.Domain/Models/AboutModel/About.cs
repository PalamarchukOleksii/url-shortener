namespace UrlShortener.Domain.Models.AboutModel;

public class About
{
    public AboutId Id { get; init; } = new AboutId(Guid.NewGuid());
    public LanguageCode Language { get; init; } = LanguageCode.En;
    public string Description { get; init; } = string.Empty;
    public DateTime LastEditAt { get; init; } = DateTime.UtcNow;
}