using Microsoft.AspNetCore.Identity;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Domain.Enums;

namespace WebStoreUser.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
    IPasswordHasher<User> passwordHasher,
    IHashService hashService,
    ITokenGenerator tokenGenerator) : IAuthService
{
    // Interface implementation
    public async Task<TokenResponseDto> LoginAsync(UserLoginDto request)
    {
        if (String.IsNullOrWhiteSpace(request.Login))
            return null;

        User? user;
        if (request.Login.Contains('@'))
            user = await userRepository.GetByEmailAsync(request.Login);
        else
            user = await userRepository.GetByUsernameAsync(request.Login);
        if (user is null) return null;

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        var isVerified = result != PasswordVerificationResult.Failed;
        if (!isVerified) return null;

        var response = tokenGenerator.CreateTokens(user);

        var salt = hashService.GenerateSalt();
        var refreshTokenHash = hashService.HashToken(response.RefreshToken, salt);

        var session = new Session
        {
            UserId = user.Id,
            RefreshTokenHash = refreshTokenHash,
            Salt = Convert.ToBase64String(salt),
            Expires = DateTime.UtcNow.AddDays(14),
            IsRevoked = false
        };

        await sessionRepository.CreateAsync(session);
        await sessionRepository.SaveChangesAsync();
        return response;
    }

    public async Task<bool> LogoutAsync()
    {
        throw new NotImplementedException(); // TODO: Logout
    }

    public async Task<TokenResponseDto> RefreshAsync(RefreshTokenRequestDto request)
    {
        var user = await userRepository.GetByIdAsync(request.Guid);
        if (user == null) return null;

        var sessionList = (await sessionRepository.GetAllActiveByIdAsync(request.Guid))?.ToList();
        if (sessionList == null) return null;

        var session = sessionList
            .FirstOrDefault(s => hashService.VerifyHashToken(request.RefreshToken, Convert.FromBase64String(s.Salt), s.RefreshTokenHash));
        if (session == null) return null;

        var response = tokenGenerator.CreateTokens(user);

        var salt = hashService.GenerateSalt();
        var refreshTokenHash = hashService.HashToken(response.RefreshToken, salt);

        session.RefreshTokenHash = refreshTokenHash;
        session.Salt = Convert.ToBase64String(salt);
        session.Expires = DateTime.UtcNow.AddDays(7);

        await sessionRepository.SaveChangesAsync();
        return response;
    }

    public async Task<bool> RegisterAsync(UserRegisterDto request)
    {
        if (await userRepository.GetByEmailAsync(request.Email) != null
            || await userRepository.GetByUsernameAsync(request.Username) != null)
        {
            return false;
        }

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Customer
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        await userRepository.CreateAsync(user);
        await sessionRepository.SaveChangesAsync();
        return true;
    }
}
