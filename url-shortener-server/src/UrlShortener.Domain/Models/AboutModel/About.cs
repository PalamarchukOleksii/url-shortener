namespace UrlShortener.Domain.Models.AboutModel;

public class About
{
    public AboutId Id { get; init; } = new(Guid.NewGuid());
    public LanguageCode Language { get; init; } = LanguageCode.En;
    public string Description { get; set; } = string.Empty;
    public DateTime LastEditAt { get; set; } = DateTime.UtcNow;

    public void Update(string description)
    {
        Description = description;
        LastEditAt = DateTime.UtcNow;
    }
}