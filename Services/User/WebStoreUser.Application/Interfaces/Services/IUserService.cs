using WebStoreUser.Application.Dtos;

namespace WebStoreUser.Application.Interfaces.Services;

public interface IUserService
{
    Task<bool> RegisterAsync(UserRegisterDto dto);
    Task<TokenResponseDto> LoginAsync();
    Task<bool> LogoutAsync();
    Task<TokenResponseDto> RefreshAsync();
}
