using WebStoreUser.Application.Dtos;
using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid guid);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(UserRegisterDto userRegisterDto);
    Task<bool> VerifyPasswordAsync(User user, string providedPassword);
    Task<bool> ChangePasswordAsync(Guid userId, string password);
    Task<bool> ChangeEmailAsync(Guid userId, string email);
}
