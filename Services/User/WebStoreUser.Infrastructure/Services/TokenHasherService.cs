using System.Security.Cryptography;
using System.Text;
using WebStoreUser.Application.Interfaces.Services;

namespace WebStoreUser.Infrastructure.Services;

public class TokenHasherService : ITokenHasherService
{
    public byte[] GenerateSalt(int length = 32)
    {
        var salt = new byte[length];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    public string HashToken(string token, byte[] salt, int iterations = 100_000)
    {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
        byte[] derivedKey = Rfc2898DeriveBytes.Pbkdf2(tokenBytes, salt, iterations, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(derivedKey);
    }

    public bool VerifyHashToken(string token, byte[] salt, string hashedToken)
    {
        return HashToken(token, salt) == hashedToken;
    }
}
