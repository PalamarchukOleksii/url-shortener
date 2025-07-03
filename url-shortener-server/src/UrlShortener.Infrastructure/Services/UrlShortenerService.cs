using System.Security.Cryptography;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Infrastructure.Services;

public class UrlShortenerService(IShortenedUrlRepository shortenedUrlRepository) : IUrlShortenerService
{
    private const int NumberOfCharsInShortCode = 7;

    private static readonly char[] Alphabet =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public async Task<string?> CreateShortCodeAsync()
    {
        const int maxAttempts = 10;
        var shortCode = string.Empty;
        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            shortCode = GenerateRandomCode();
            if (!await shortenedUrlRepository.ExistsByShortCodeAsync(shortCode)) return shortCode;
        }

        return shortCode;
    }

    private static string GenerateRandomCode()
    {
        var bytes = new byte[NumberOfCharsInShortCode];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        var chars = new char[NumberOfCharsInShortCode];
        for (var i = 0; i < NumberOfCharsInShortCode; i++) chars[i] = Alphabet[bytes[i] % Alphabet.Length];
        return new string(chars);
    }
}