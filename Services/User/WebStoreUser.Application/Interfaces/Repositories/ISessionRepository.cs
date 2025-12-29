using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<IQueryable<Session>?> GetAllActiveByIdAsync(Guid userId);
    Task CreateAsync(Session session);
    Task SaveChangesAsync();
}
