using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// User management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
 {
  var users = await _userService.GetAllUsersAsync();
   return Ok(users);
    }

    /// <summary>
    /// Get user by ID
 /// </summary>
    [HttpGet("{id}")]
 public async Task<ActionResult<AppUser>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
  
        if (user == null)
     return NotFound($"User with ID '{id}' not found.");

  return Ok(user);
    }

    /// <summary>
    /// Get user with roles by ID
    /// </summary>
    [HttpGet("{id}/roles")]
    public async Task<ActionResult<AppUser>> GetUserWithRoles(Guid id)
    {
        var user = await _userService.GetUserWithRolesAsync(id);
        
   if (user == null)
   return NotFound($"User with ID '{id}' not found.");

    return Ok(user);
    }

    /// <summary>
    /// Get user by username
 /// </summary>
    [HttpGet("username/{username}")]
    public async Task<ActionResult<AppUser>> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        
if (user == null)
       return NotFound($"User with username '{username}' not found.");

        return Ok(user);
  }

    /// <summary>
    /// Create new user
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AppUser>> CreateUser([FromBody] CreateUserRequest request)
    {
        try
      {
        var user = await _userService.CreateUserAsync(
    request.Username, 
       request.Password, 
           request.FullName, 
  request.Email);
            
         return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
    }
        catch (InvalidOperationException ex)
     {
  return BadRequest(ex.Message);
 }
    }

    /// <summary>
    /// Update user
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<AppUser>> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        try
        {
var user = await _userService.UpdateUserAsync(id, request.FullName, request.Email, request.IsActive);
   return Ok(user);
  }
    catch (InvalidOperationException ex)
        {
   return NotFound(ex.Message);
        }
    }

    /// <summary>
  /// Delete user
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await _userService.DeleteUserAsync(id);
        
        if (!result)
      return NotFound($"User with ID '{id}' not found.");

        return NoContent();
    }

    /// <summary>
    /// Change user password
    /// </summary>
    [HttpPost("{id}/change-password")]
    public async Task<ActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request)
    {
     var result = await _userService.ChangePasswordAsync(id, request.OldPassword, request.NewPassword);
        
        if (!result)
            return BadRequest("Invalid old password or user not found.");

        return Ok("Password changed successfully.");
 }

    /// <summary>
    /// Check if username exists
    /// </summary>
    [HttpGet("check-username/{username}")]
    public async Task<ActionResult<bool>> CheckUsernameExists(string username)
    {
     var exists = await _userService.UsernameExistsAsync(username);
        return Ok(new { exists });
    }
}

// Request models
public class CreateUserRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Email { get; set; }
}

public class UpdateUserRequest
{
    public string FullName { get; set; } = null!;
    public string? Email { get; set; }
    public bool IsActive { get; set; }
}

public class ChangePasswordRequest
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
