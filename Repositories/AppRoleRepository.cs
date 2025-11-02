using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Role repository implementation for managing AppRole entities
/// </summary>
public class AppRoleRepository : IAppRoleRepository
{
    private readonly Prn232GradingContext _context;

 public AppRoleRepository(Prn232GradingContext context)
    {
      _context = context;
    }

public async Task<IEnumerable<AppRole>> GetAllAsync()
    {
 return await _context.AppRoles
          .Include(r => r.Users)
    .OrderBy(r => r.RoleName)
            .ToListAsync();
    }

  public async Task<AppRole?> GetByIdAsync(int roleId)
  {
        return await _context.AppRoles
            .Include(r => r.Users)
      .FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    public async Task<AppRole?> GetByRoleNameAsync(string roleName)
    {
        return await _context.AppRoles
    .Include(r => r.Users)
         .FirstOrDefaultAsync(r => r.RoleName == roleName);
    }

    public async Task<bool> RoleNameExistsAsync(string roleName)
    {
        return await _context.AppRoles
     .AnyAsync(r => r.RoleName == roleName);
    }

    public async Task AddAsync(AppRole role)
    {
        await _context.AppRoles.AddAsync(role);
    }

    public void Update(AppRole role)
    {
  _context.AppRoles.Update(role);
    }

 public void Delete(AppRole role)
    {
 _context.AppRoles.Remove(role);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
