namespace WebStoreUser.Application.Dtos;

public record UserSessionDto(
    string? Device,
    string? Ip,
    string? UserAgent,
    DateTime LastUpdated
    );