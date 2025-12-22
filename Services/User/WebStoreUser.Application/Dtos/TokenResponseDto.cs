namespace WebStoreUser.Application.Dtos;

public sealed record TokenResponseDto(string AccessToken, string RefreshToken);
