namespace WebStoreUser.Application.Interfaces.Services;

public interface IHashService
{
    byte[] GenerateSalt(int length = 32);
    string HashToken(string token, byte[] salt, int iterations = 100_000);
    bool VerifyHashToken(string token, byte[] salt, string hashedToken);
}
