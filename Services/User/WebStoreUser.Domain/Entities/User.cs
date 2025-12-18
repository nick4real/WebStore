namespace WebStoreUser.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; init; }
}
