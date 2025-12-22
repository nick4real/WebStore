using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Domain.Enums;
using WebStoreUser.Infrastructure.Persistence;

namespace WebStoreUser.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    public async Task<User> AddAsync(UserRegisterDto dto)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Customer
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ChangeEmailAsync(Guid userId, string email)
    {
        throw new NotImplementedException(); // TODO: Change user email
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string password)
    {
        throw new NotImplementedException(); // TODO: Change user password
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByIdAsync(Guid guid)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == guid);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> VerifyPasswordAsync(User user, string providedPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
        return result != PasswordVerificationResult.Failed;
    }
}
