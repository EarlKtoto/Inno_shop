using FluentValidation;
using Inno_Shop.Domain.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50);

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");

        RuleFor(u => u.Role)
            .IsInEnum().WithMessage("Invalid user role");

        RuleFor(u => u.IsActive)
            .NotNull()
            .WithMessage("Active status is required");
    }
}