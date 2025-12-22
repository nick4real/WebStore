namespace WebStoreUser.Application.Dtos;

public record UserRegisterDto(
    string Username,
    string Email,
    string Password
    );
