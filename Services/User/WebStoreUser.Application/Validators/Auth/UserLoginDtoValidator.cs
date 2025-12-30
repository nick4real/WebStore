using FluentValidation;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Repositories;

namespace WebStoreUser.Application.Validators.Auth;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MinimumLength(3).WithMessage("Login must be at least 3 characters long.")
            .MaximumLength(256).WithMessage("Login must not exceed 256 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password length must be at least 8.")
            .MaximumLength(32).WithMessage("Password length must not exceed 32.");
    }
}
