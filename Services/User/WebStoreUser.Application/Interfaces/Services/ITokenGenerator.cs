using WebStoreUser.Application.Dtos;
using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Application.Interfaces.Services;

public interface ITokenGenerator
{
    TokenResponseDto CreateTokens(User user);
}
