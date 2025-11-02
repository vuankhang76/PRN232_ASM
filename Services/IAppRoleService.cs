using BusinessObjects;

namespace Services;

/// <summary>
/// AppRole service interface for business logic
/// </summary>
public interface IAppRoleService
{
    Task<IEnumerable<AppRole>> GetAllRolesAsync();
    Task<AppRole?> GetRoleByIdAsync(int roleId);
    Task<AppRole?> GetRoleByNameAsync(string roleName);
    Task<AppRole> CreateRoleAsync(string roleName);
    Task<bool> DeleteRoleAsync(int roleId);
    Task<bool> RoleNameExistsAsync(string roleName);
}
