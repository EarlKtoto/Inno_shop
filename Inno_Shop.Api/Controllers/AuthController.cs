using Application.Services;

namespace Inno_Shop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;
    private readonly EmailService _emailService;

    public AuthController(UserService userService, JwtService jwtService, EmailService emailService)
    {
        _userService = userService;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user, string password)
    {
        var created = await _userService.RegisterUserAsync(user, password);

        var link = $"https://localhost:5237/api/auth/confirm?token={created.EmailConfirmationToken}";

        await _emailService.SendEmailAsync(user.Email, "Confirm your account",
            $"Click <a href='{link}'>here</a> to activate your account");

        return Ok(new { message = "Registration successful. Please confirm email." });
    }

    [HttpGet("confirm")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        await _userService.ConfirmEmailAsync(token);
        return Ok("Email confirmed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userService.AuthenticateUserAsync(email, password);
        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        await _userService.RequestPasswordResetAsync(email);
        return Ok("Reset link sent");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(string token, string newPassword)
    {
        await _userService.ResetPasswordAsync(token, newPassword);
        return Ok("Password updated");
    }
}
