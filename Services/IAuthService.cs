using BusinessObjects;

namespace Services;

/// <summary>
/// Authentication service interface
/// </summary>
public interface IAuthService
{
    Task<(bool Success, string Token, AppUser? User)> LoginAsync(string username, string password);
    Task<AppUser> RegisterAsync(string username, string password, string fullName, string? email, List<int> roleIds);
    Task<bool> AssignRoleToUserAsync(Guid userId, int roleId);
    Task<bool> RemoveRoleFromUserAsync(Guid userId, int roleId);
}
