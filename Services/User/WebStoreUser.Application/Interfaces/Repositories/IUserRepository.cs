using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid guid);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByLoginAsync(string login);
    Task AddAsync(User user);
    Task SaveChangesAsync();
    Task<bool> IsLoginExistsAsync(string username, string email);
}
