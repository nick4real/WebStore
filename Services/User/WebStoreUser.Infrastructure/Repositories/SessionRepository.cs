using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure.Persistence;

namespace WebStoreUser.Infrastructure.Repositories;

public class SessionRepository(AppDbContext dbContext, IHashService hashService) : ISessionRepository
{
    public async Task<Session> CreateAsync(Guid userId, string refreshToken)
    {
        var salt = hashService.GenerateSalt();
        var refreshTokenHash = hashService.HashToken(refreshToken, salt);

        var session = new Session
        {
            UserId = userId,
            RefreshTokenHash = refreshTokenHash,
            Salt = Convert.ToBase64String(salt),
            Expires = DateTime.UtcNow.AddDays(14),
            IsRevoked = false
        };

        await dbContext.Sessions.AddAsync(session);
        await dbContext.SaveChangesAsync();

        return session;
    }

    public async Task<Session> GetActiveAsync(Guid userId, string refreshToken)
    {
        var sessionList = await GetActiveCollectionAsync(userId);
        if (sessionList == null) return null;

        var session = sessionList
            .FirstOrDefault(s => hashService.VerifyHashToken(refreshToken, Convert.FromBase64String(s.Salt), s.RefreshTokenHash));
        if (session == null) return null;

        return session;
    }

    public async Task<ICollection<Session>> GetActiveCollectionAsync(Guid userId)
    {
        return await dbContext.Sessions
                .Where(s => s.IsRevoked == false
                && s.UserId == userId
                && DateTime.UtcNow <= s.Expires)
                .ToListAsync();
    }

    public async Task<bool> RevokeAsync(Session session)
    {
        throw new NotImplementedException(); // TODO: Revoke session
    }

    public async Task<Session> UpdateAsync(Session session, string refreshToken)
    {
        var salt = hashService.GenerateSalt();
        var refreshTokenHash = hashService.HashToken(refreshToken, salt);

        session.RefreshTokenHash = refreshTokenHash;
        session.Salt = Convert.ToBase64String(salt);
        session.Expires = DateTime.UtcNow.AddDays(14);

        await dbContext.SaveChangesAsync();
        return session;
    }
}
