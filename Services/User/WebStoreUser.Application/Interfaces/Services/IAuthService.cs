using WebStoreUser.Application.Requests;
using WebStoreUser.Application.Responses;

namespace WebStoreUser.Application.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterRequest dto);
    Task<TokenResponse> LoginAsync(UserLoginRequest dto);
    Task<bool> LogoutAsync();
    Task<TokenResponse> RefreshAsync(RefreshTokenRequest dto);
}
