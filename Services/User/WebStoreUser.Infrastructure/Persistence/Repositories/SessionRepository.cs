using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure;

namespace WebStoreUser.Infrastructure.Persistence.Repositories;

public class SessionRepository(AppDbContext dbContext) : ISessionRepository
{
    public async Task AddAsync(Session session)
        => await dbContext.Sessions.AddAsync(session);

    public async Task<IReadOnlyList<Session>?> GetAllActiveByIdAsync(Guid userId)
        => await dbContext.Sessions
        .Where(s => s.IsRevoked == false
        && s.UserId == userId
        && DateTime.UtcNow <= s.Expires)
        .ToListAsync();

    public async Task SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}
