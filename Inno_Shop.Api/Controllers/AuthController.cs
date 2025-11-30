using Application.Services;
using Application.DTOs;
using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Enums;

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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Role = request.Role.HasValue ? (UserRole)request.Role.Value : UserRole.User,
            IsActive = false
        };

        var created = await _userService.RegisterUserAsync(user, request.Password);

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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);
        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.RequestPasswordResetAsync(request.Email);
        return Ok("Reset link sent");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.ResetPasswordAsync(request.Token, request.NewPassword);
        return Ok("Password updated");
    }
}
