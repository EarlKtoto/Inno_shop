using Inno_Shop.Domain.Entities;
using Inno_Shop.Infrastructure.Data.Repositories;

namespace Application.Services;

public class UserService
{
    readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterUserAsync(User user, string password)
    {
        if(_userRepository.GetByIdAsync(user.Id) != null)
            throw new Exception("User already exists");

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

    public IEnumerable<User> GetAllUsersInformation()
    {
        return _userRepository.GetAllAsync().Result;
    }
}