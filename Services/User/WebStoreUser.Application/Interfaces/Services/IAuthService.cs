using WebStoreUser.Application.Requests;
using WebStoreUser.Application.Responses;

namespace WebStoreUser.Application.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterRequest dto, CancellationToken ct);
    Task<TokenResponse> LoginAsync(UserLoginRequest dto, CancellationToken ct);
    Task<bool> LogoutAsync();
    Task<TokenResponse> RefreshAsync(RefreshTokenRequest dto, CancellationToken ct);
}
