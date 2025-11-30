namespace Application.DTOs;

public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int? Role { get; set; }
}

