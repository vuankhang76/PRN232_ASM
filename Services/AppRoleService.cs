using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// AppRole service implementation for business logic
/// </summary>
public class AppRoleService : IAppRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppRoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AppRole>> GetAllRolesAsync()
    {
     return await _unitOfWork.AppRoleRepository.GetAllAsync();
  }

    public async Task<AppRole?> GetRoleByIdAsync(int roleId)
    {
        return await _unitOfWork.AppRoleRepository.GetByIdAsync(roleId);
    }

    public async Task<AppRole?> GetRoleByNameAsync(string roleName)
    {
   return await _unitOfWork.AppRoleRepository.GetByRoleNameAsync(roleName);
    }

    public async Task<AppRole> CreateRoleAsync(string roleName)
    {
      // Validate role name doesn't exist
     if (await _unitOfWork.AppRoleRepository.RoleNameExistsAsync(roleName))
    {
     throw new InvalidOperationException($"Role '{roleName}' already exists.");
  }

  var role = new AppRole
   {
        RoleName = roleName
  };

        await _unitOfWork.AppRoleRepository.AddAsync(role);
   await _unitOfWork.SaveChangesAsync();

  return role;
  }

    public async Task<bool> DeleteRoleAsync(int roleId)
    {
        var role = await _unitOfWork.AppRoleRepository.GetByIdAsync(roleId);
  
if (role == null)
     return false;

      _unitOfWork.AppRoleRepository.Delete(role);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> RoleNameExistsAsync(string roleName)
    {
        return await _unitOfWork.AppRoleRepository.RoleNameExistsAsync(roleName);
    }
}
