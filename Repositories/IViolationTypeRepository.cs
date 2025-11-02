using BusinessObjects;

namespace Repositories;

/// <summary>
/// ViolationType repository interface for managing violation types
/// </summary>
public interface IViolationTypeRepository
{
    Task<IEnumerable<ViolationType>> GetAllAsync();
    Task<ViolationType?> GetByIdAsync(int violationTypeId);
  Task<ViolationType?> GetByCodeAsync(string code);
    Task<bool> CodeExistsAsync(string code);
    Task AddAsync(ViolationType violationType);
    void Update(ViolationType violationType);
    void Delete(ViolationType violationType);
    Task<bool> SaveChangesAsync();
}
