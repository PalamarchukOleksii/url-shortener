namespace UrlShortener.Application.Interfaces.Security;

public interface IHasher
{
    Task<string?> HashAsync(string value);
    Task<bool> VerifyAsync(string value, string valueHash);
}