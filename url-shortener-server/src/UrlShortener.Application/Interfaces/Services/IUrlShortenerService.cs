namespace UrlShortener.Application.Interfaces.Services;

public interface IUrlShortenerService
{
    Task<string?> CreateShortCodeAsync();
}