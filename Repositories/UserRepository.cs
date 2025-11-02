using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// User repository implementation for managing AppUser entities
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly Prn232GradingContext _context;

    public UserRepository(Prn232GradingContext context)
    {
    _context = context;
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.AppUsers
            .Where(u => u.IsActive)
 .OrderBy(u => u.FullName)
         .ToListAsync();
 }

    public async Task<AppUser?> GetByIdAsync(Guid userId)
    {
        return await _context.AppUsers
     .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        return await _context.AppUsers
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<AppUser?> GetUserWithRolesAsync(Guid userId)
    {
   return await _context.AppUsers
            .Include(u => u.Roles)
     .FirstOrDefaultAsync(u => u.UserId == userId);
}

    public async Task<IEnumerable<AppUser>> GetAllUsersWithRolesAsync()
    {
  return await _context.AppUsers
       .Include(u => u.Roles)
 .Where(u => u.IsActive)
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.AppUsers
         .AnyAsync(u => u.Username == username);
    }

    public async Task AddAsync(AppUser user)
    {
        await _context.AppUsers.AddAsync(user);
    }

    public void Update(AppUser user)
    {
        _context.AppUsers.Update(user);
  }

    public void Delete(AppUser user)
    {
        _context.AppUsers.Remove(user);
    }

    public async Task<bool> SaveChangesAsync()
    {
  return await _context.SaveChangesAsync() > 0;
    }
}
