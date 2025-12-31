using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<IEnumerable<Session>?> GetAllActiveByIdAsync(Guid userId);
    Task AddAsync(Session session);
    Task SaveChangesAsync();
}
