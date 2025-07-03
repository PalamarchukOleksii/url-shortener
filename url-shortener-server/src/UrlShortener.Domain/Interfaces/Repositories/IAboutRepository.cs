using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Domain.Interfaces.Repositories;

public interface IAboutRepository : IRepository<About, AboutId>
{
    Task<About?> GetByLanguageCode(LanguageCode languageCode);
}