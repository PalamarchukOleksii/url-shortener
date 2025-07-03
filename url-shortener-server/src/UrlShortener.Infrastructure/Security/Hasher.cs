using System.Security.Cryptography;
using System.Text;
using UrlShortener.Application.Interfaces.Security;

namespace UrlShortener.Infrastructure.Security;

public class Hasher : IHasher
{
    private const int HashSize = 64;
    private const int Iterations = 600_000;
    private const int SaltSize = 32;
    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

    public async Task<string?> HashAsync(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        try
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var valueBytes = Encoding.UTF8.GetBytes(value);

            var hash = await Task.Run(() =>
                Rfc2898DeriveBytes.Pbkdf2(valueBytes, salt, Iterations, _algorithm, HashSize));

            var result = $"pbkdf2_sha512${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";

            Array.Clear(valueBytes);
            Array.Clear(hash);

            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> VerifyAsync(string value, string valueHash)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(valueHash))
            return false;

        try
        {
            var parts = valueHash.Split('$');
            if (parts is not ["pbkdf2_sha512", _, _, _])
                return false;

            if (!int.TryParse(parts[1], out var iterations) || iterations < 1)
                return false;

            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var valueBytes = Encoding.UTF8.GetBytes(value);

            var actualHash = await Task.Run(() =>
                Rfc2898DeriveBytes.Pbkdf2(valueBytes, salt, iterations, _algorithm, expectedHash.Length));

            var result = CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);

            Array.Clear(valueBytes);
            Array.Clear(actualHash);

            return result;
        }
        catch
        {
            return false;
        }
    }
}