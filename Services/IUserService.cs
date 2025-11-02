using BusinessObjects;

namespace Services;

/// <summary>
/// User service interface for business logic
/// </summary>
public interface IUserService
{
    Task<IEnumerable<AppUser>> GetAllUsersAsync();
    Task<AppUser?> GetUserByIdAsync(Guid userId);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<AppUser?> GetUserWithRolesAsync(Guid userId);
    Task<AppUser> CreateUserAsync(string username, string password, string fullName, string? email = null);
    Task<AppUser> UpdateUserAsync(Guid userId, string fullName, string? email, bool isActive);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    Task<bool> UsernameExistsAsync(string username);
}
