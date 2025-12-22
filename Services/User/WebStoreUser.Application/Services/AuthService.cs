using Microsoft.AspNetCore.Identity;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    ISessionRepository sessionRepository,
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

        var isVerified = await userRepository.VerifyPasswordAsync(user, request.Password);
        if (!isVerified) return null;

        var response = tokenGenerator.CreateTokens(user);
        await sessionRepository.CreateAsync(user.Id, response.RefreshToken);

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

        var session = await sessionRepository.GetActiveAsync(request.Guid, request.RefreshToken);
        if (session == null) return null;

        var response = tokenGenerator.CreateTokens(user);
        await sessionRepository.UpdateAsync(session, response.RefreshToken);

        return response;
    }

    public async Task<bool> RegisterAsync(UserRegisterDto request)
    {
        if (await userRepository.GetByEmailAsync(request.Email) != null
            || await userRepository.GetByUsernameAsync(request.Username) != null)
        {
            return false;
        }

        await userRepository.AddAsync(request);
        return true;
    }
}
