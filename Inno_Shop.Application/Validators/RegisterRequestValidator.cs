using Application.DTOs;
using FluentValidation;
using Inno_Shop.Domain.Enums;

namespace Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid")
            .MaximumLength(50).WithMessage("Email must be at most 50 characters");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(r => r.Role)
            .Must(role => role == null || Enum.IsDefined(typeof(UserRole), role.Value))
            .WithMessage("Invalid user role");
    }
}

