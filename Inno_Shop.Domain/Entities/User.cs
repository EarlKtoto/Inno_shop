using System.ComponentModel.DataAnnotations;
using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Enums;

public class User
{
    [Key]
    public long Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; }

    [Required, StringLength(50)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsActive { get; set; } = false;

    public string? EmailConfirmationToken { get; set; }
    
    public string? PasswordResetToken { get; set; }
    
    public DateTimeOffset? PasswordResetTokenExpires { get; set; }

    public ICollection<Product> Products { get; set; }
}
