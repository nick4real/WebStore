using WebStoreUser.Application.Dtos;

namespace WebStoreUser.Application.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterDto dto);
    Task<TokenResponseDto> LoginAsync(UserLoginDto dto);
    Task<bool> LogoutAsync();
    Task<TokenResponseDto> RefreshAsync(RefreshTokenRequestDto dto);
}
