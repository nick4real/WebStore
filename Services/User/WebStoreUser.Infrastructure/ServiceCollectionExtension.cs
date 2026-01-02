using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStoreUser.Application.Interfaces.Repositories;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Application.Services;
using WebStoreUser.Application.Validators.Auth;
using WebStoreUser.Domain.Entities;
using WebStoreUser.Infrastructure.Persistence;
using WebStoreUser.Infrastructure.Repositories;
using WebStoreUser.Infrastructure.Services;

namespace WebStoreUser.Infrastructure;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDatabase"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            // Validators
            services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>();
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
