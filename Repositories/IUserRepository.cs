using BusinessObjects;

namespace Repositories;

/// <summary>
/// User repository interface for managing AppUser entities
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser?> GetByIdAsync(Guid userId);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<AppUser?> GetUserWithRolesAsync(Guid userId);
    Task<IEnumerable<AppUser>> GetAllUsersWithRolesAsync();
    Task<bool> UsernameExistsAsync(string username);
    Task AddAsync(AppUser user);
    void Update(AppUser user);
    void Delete(AppUser user);
    Task<bool> SaveChangesAsync();
}
