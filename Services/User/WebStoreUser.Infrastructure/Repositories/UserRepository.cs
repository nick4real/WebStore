using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure.Persistence;

namespace WebStoreUser.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user)
        => await dbContext.Users.AddAsync(user);

    public async Task<User?> GetByEmailAsync(string email)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid guid)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == guid);

    public async Task<User?> GetByLoginAsync(string login)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Username == login || u.Email == login);

    public async Task<User?> GetByUsernameAsync(string username)
        => await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<bool> IsLoginExistsAsync(string username, string email)
        => await dbContext.Users.AnyAsync(u => u.Username == username || u.Email == email);

    public async Task SaveChangesAsync() 
        => await dbContext.SaveChangesAsync();
}
