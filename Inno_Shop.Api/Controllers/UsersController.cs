namespace Inno_Shop.Api.Controllers;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _userService.GetAllUsersInformationAsync());

    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> Activate(long id)
    {
        await _userService.ActivateUserAsync(id);
        return Ok("User activated");
    }

    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(long id)
    {
        await _userService.DeactivateUserAsync(id);
        return Ok("User deactivated");
    }
}
