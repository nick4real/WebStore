namespace WebStoreUser.Application.Requests;

public sealed record RefreshTokenRequest(Guid Guid, string RefreshToken);