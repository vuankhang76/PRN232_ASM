using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services;

/// <summary>
/// Authentication service implementation
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<(bool Success, string Token, AppUser? User)> LoginAsync(string username, string password)
    {
        var user = await _unitOfWork.UserRepository.GetUserWithRolesAsync(
            (await _unitOfWork.UserRepository.GetByUsernameAsync(username))?.UserId ?? Guid.Empty);

        if (user == null || !user.IsActive)
         return (false, string.Empty, null);

    // Verify password
if (user.PasswordHash == null || !VerifyPassword(password, user.PasswordHash))
            return (false, string.Empty, null);

        // Generate JWT token
        var token = GenerateJwtToken(user);
return (true, token, user);
    }

    public async Task<AppUser> RegisterAsync(string username, string password, string fullName, string? email, List<int> roleIds)
    {
      // Check if username exists
if (await _unitOfWork.UserRepository.UsernameExistsAsync(username))
            throw new InvalidOperationException($"Username '{username}' already exists.");

 var user = new AppUser
   {
            UserId = Guid.NewGuid(),
  Username = username,
            PasswordHash = HashPassword(password),
FullName = fullName,
            Email = email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
 };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

   // Assign roles if provided
        if (roleIds != null && roleIds.Any())
  {
            foreach (var roleId in roleIds)
            {
  await AssignRoleToUserAsync(user.UserId, roleId);
        }
        }

      return user;
    }

    public async Task<bool> AssignRoleToUserAsync(Guid userId, int roleId)
    {
        var user = await _unitOfWork.UserRepository.GetUserWithRolesAsync(userId);
        if (user == null)
            return false;

    var role = await _unitOfWork.AppRoleRepository.GetByIdAsync(roleId);
 if (role == null)
   return false;

        if (!user.Roles.Any(r => r.RoleId == roleId))
        {
            user.Roles.Add(role);
   await _unitOfWork.SaveChangesAsync();
        }

    return true;
    }

    public async Task<bool> RemoveRoleFromUserAsync(Guid userId, int roleId)
    {
        var user = await _unitOfWork.UserRepository.GetUserWithRolesAsync(userId);
     if (user == null)
          return false;

        var role = user.Roles.FirstOrDefault(r => r.RoleId == roleId);
        if (role != null)
        {
      user.Roles.Remove(role);
   await _unitOfWork.SaveChangesAsync();
  }

        return true;
    }

    private string GenerateJwtToken(AppUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
   var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
    var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
   var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
       new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
         new Claim(ClaimTypes.Name, user.Username),
 new Claim(ClaimTypes.Email, user.Email ?? ""),
      new Claim("FullName", user.FullName)
        };

        // Add roles to claims
        foreach (var role in user.Roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        }

     var token = new JwtSecurityToken(
      issuer: issuer,
            audience: audience,
        claims: claims,
         expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
      );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private byte[] HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
  return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] hash)
    {
        var computedHash = HashPassword(password);
        return computedHash.SequenceEqual(hash);
    }
}
