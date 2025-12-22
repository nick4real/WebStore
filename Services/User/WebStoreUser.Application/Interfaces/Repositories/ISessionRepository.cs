using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<Session> GetActiveAsync(Guid userId, string refreshToken);
    Task<ICollection<Session>> GetActiveCollectionAsync(Guid userId);
    Task<Session> CreateAsync(Guid userId, string refreshToken);
    Task<Session> UpdateAsync(Session session, string refreshToken);
    Task<bool> RevokeAsync(Session session);
}
