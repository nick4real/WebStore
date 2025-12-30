using Microsoft.AspNetCore.Identity;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Application.Validators.Auth;
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
        if (request.Login.Length > 20 && !request.Login.IsEmail())
            return null;

        var user = await userRepository.GetByLoginAsync(request.Login);
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

        await sessionRepository.AddAsync(session);
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
        // TODO: Can be optimized when SharpGrip.FluentValidation.AutoValidation.Mvc support for .NET 10 is released
        if (await userRepository.IsLoginExistsAsync(request.Username, request.Email))
            return false;

        var user = new User() 
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Customer
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        await userRepository.AddAsync(user);
        await sessionRepository.SaveChangesAsync();
        return true;
    }
}
