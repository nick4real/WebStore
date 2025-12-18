namespace WebStoreUser.Domain.Entities;

public class Session
{
    public uint Id { get; init; }
    public required Guid UserId { get; init; }
    public required string RefreshTokenHash { get; set; }
    public required string Salt { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string? Device { get; set; }
    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
}
