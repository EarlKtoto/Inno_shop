using Inno_Shop.Domain.Entities;
using Inno_Shop.Domain.Interfaces;
using Inno_Shop.Infrastructure.Data.Repositories;

namespace Application.Services;

public class UserService
{
    readonly IUserRepository _userRepository;
    readonly UserValidator _userValidator;

    public UserService(IUserRepository userRepository, UserValidator userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<User> RegisterUserAsync(User user, string password)
    {
        _userValidator.Validate(user);
        
        var existingUser = await _userRepository.GetByEmailAsync(user.Email);
        if(existingUser != null)
            throw new Exception("User with this email already exists");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        user.EmailConfirmationToken = Guid.NewGuid().ToString();
        
        await _userRepository.CreateAsync(user);

        return user;    
    }

    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        
        if(user == null)
            throw new Exception("Invalid credentials");
        
        if(!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials");
        
        if(user.IsActive == false)
            throw new Exception("Is not active");
        
        return user;
    }

    public async Task ConfirmEmailAsync(string token)
    {
        var user = await _userRepository.GetByConfirmationTokenAsync(token);
        
        if(user == null)
            throw new Exception("Invalid token");

        user.IsActive = true;
        user.EmailConfirmationToken = null;
        await _userRepository.UpdateAsync(user);
    }

    public async Task RequestPasswordResetAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if(user == null)
            throw new Exception("User not found");
        
        user.PasswordResetToken = Guid.NewGuid().ToString();
        user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);
        
        await _userRepository.UpdateAsync(user);
    }

    public async Task ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _userRepository.GetByPasswordResetTokenAsync(token);
        if(user == null)
            throw new Exception("Invalid token");
        
        if(user.PasswordResetTokenExpires < DateTime.UtcNow)
            throw new Exception("Password reset token expired");
        
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpires = null;
        
        await _userRepository.UpdateAsync(user);
    }

    public async Task ActivateUserAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        user.IsActive = true;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeactivateUserAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        _userValidator.Validate(user);
        if(_userRepository.GetByIdAsync(user.Id) == null)
            throw new Exception("User not found");
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        if(_userRepository.GetByIdAsync(user.Id) == null)
            throw new Exception("User not found");
        await _userRepository.DeleteAsync(user);
    }

    public async Task<User> GetUserInformationAsync(int id)
    {
        if(_userRepository.GetByIdAsync(id) == null)
            throw new Exception("User not found");
        return _userRepository.GetByIdAsync(id).Result;
    }

    public async Task<List<User>> GetAllUsersInformationAsync()
    {
        return _userRepository.GetAllAsync().Result;
    }
}