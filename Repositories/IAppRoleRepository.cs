using BusinessObjects;

namespace Repositories;

/// <summary>
/// Role repository interface for managing AppRole entities
/// </summary>
public interface IAppRoleRepository
{
    Task<IEnumerable<AppRole>> GetAllAsync();
    Task<AppRole?> GetByIdAsync(int roleId);
    Task<AppRole?> GetByRoleNameAsync(string roleName);
    Task<bool> RoleNameExistsAsync(string roleName);
    Task AddAsync(AppRole role);
    void Update(AppRole role);
    void Delete(AppRole role);
    Task<bool> SaveChangesAsync();
}
