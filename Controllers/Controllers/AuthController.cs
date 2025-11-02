using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login endpoint
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
      var (success, token, user) = await _authService.LoginAsync(request.Username, request.Password);

      if (!success)
   return Unauthorized(new { message = "Invalid username or password" });

   return Ok(new
        {
token,
            user = new
        {
     user!.UserId,
  user.Username,
    user.FullName,
        user.Email,
     roles = user.Roles.Select(r => r.RoleName).ToList()
        }
        });
 }

    /// <summary>
    /// Register new user
    /// </summary>
[HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        try
   {
   var user = await _authService.RegisterAsync(
      request.Username,
         request.Password,
request.FullName,
    request.Email,
   request.RoleIds ?? new List<int>());

    return Ok(new
      {
     message = "User registered successfully",
userId = user.UserId
            });
  }
catch (InvalidOperationException ex)
        {
      return BadRequest(new { message = ex.Message });
}
  }

    /// <summary>
    /// Assign role to user (Admin only)
    /// </summary>
    [HttpPost("assign-role")]
    public async Task<ActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        var success = await _authService.AssignRoleToUserAsync(request.UserId, request.RoleId);

 if (!success)
            return BadRequest(new { message = "Failed to assign role" });

        return Ok(new { message = "Role assigned successfully" });
    }
}

// Request models
public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterRequest
{
  public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
  public string FullName { get; set; } = null!;
    public string? Email { get; set; }
    public List<int>? RoleIds { get; set; }
}

public class AssignRoleRequest
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
