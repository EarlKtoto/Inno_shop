using System.ComponentModel.DataAnnotations;
using Inno_Shop.Domain.Enums;

namespace Inno_Shop.Domain.Entities;

public class Product
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }

    [Required] 
    public decimal Price { get; set; } 

    [Required]
    public ProductAvailability Availability { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public DateTimeOffset CreatedOn { get; set; }
    
    public User User { get; set; }
}