using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure.Persistence;

namespace WebStoreUser.Infrastructure.Repositories;

public class SessionRepository(AppDbContext dbContext) : ISessionRepository
{
    public async Task AddAsync(Session session)
    {
        await dbContext.Sessions.AddAsync(session);
    }

    public async Task<IQueryable<Session>?> GetAllActiveByIdAsync(Guid userId)
    {
        return dbContext.Sessions
                .Where(s => s.IsRevoked == false
                && s.UserId == userId
                && DateTime.UtcNow <= s.Expires);
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}
