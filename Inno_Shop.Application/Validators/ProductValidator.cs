using FluentValidation;
using Inno_Shop.Domain.Entities;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(50).WithMessage("Product name must be at most 50 characters");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description must be at most 500 characters");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(p => p.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative");

        RuleFor(p => p.Availability)
            .IsInEnum().WithMessage("Invalid product availability value");

        RuleFor(p => p.UserId)
            .NotNull().WithMessage("UserId is required");

        RuleFor(p => p.CreatedOn)
            .NotEmpty().WithMessage("CreatedOn is required")
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .WithMessage("Creation date cannot be in the future");
    }
}