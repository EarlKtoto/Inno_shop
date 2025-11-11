using System.ComponentModel.DataAnnotations;
using Inno_Shop.Domain.Enums;

namespace Inno_Shop.Domain.Entities;

public class User
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
    
    public ICollection<Product> Products { get; set; }
}