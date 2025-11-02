using BusinessObjects;

namespace Repositories;

/// <summary>
/// Semester repository interface for managing Semester entities
/// </summary>
public interface ISemesterRepository
{
    Task<IEnumerable<Semester>> GetAllAsync();
    Task<Semester?> GetByIdAsync(int semesterId);
    Task<Semester?> GetByCodeAsync(string code);
    Task<bool> CodeExistsAsync(string code);
    Task AddAsync(Semester semester);
    void Update(Semester semester);
    void Delete(Semester semester);
  Task<bool> SaveChangesAsync();
}
