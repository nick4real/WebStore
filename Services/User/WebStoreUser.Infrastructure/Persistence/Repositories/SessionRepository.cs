using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure;

namespace WebStoreUser.Infrastructure.Persistence.Repositories;

public class SessionRepository(AppDbContext dbContext) : ISessionRepository
{
    public async Task AddAsync(Session session, CancellationToken ct)
        => await dbContext.Sessions.AddAsync(session, ct);

    public async Task<IReadOnlyList<Session>?> GetAllActiveByIdAsync(Guid userId, CancellationToken ct)
        => await dbContext.Sessions
        .Where(s => !s.IsRevoked && s.UserId == userId && DateTime.UtcNow <= s.Expires)
        .ToListAsync(ct);

    public async Task SaveChangesAsync(CancellationToken ct)
        => await dbContext.SaveChangesAsync(ct);
}
