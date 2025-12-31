using WebStoreUser.Domain.Common;

namespace WebStoreUser.Domain.Entities;

public class Session : BaseEntity<uint>
{
    public Guid UserId { get; set; }
    public string RefreshTokenHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
    public string? Device { get; set; }
    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
}
