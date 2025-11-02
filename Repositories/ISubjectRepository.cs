using BusinessObjects;

namespace Repositories;

/// <summary>
/// Subject repository interface for managing Subject entities
/// </summary>
public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<Subject?> GetByIdAsync(int subjectId);
    Task<Subject?> GetByCodeAsync(string code);
  Task<bool> CodeExistsAsync(string code);
    Task AddAsync(Subject subject);
    void Update(Subject subject);
    void Delete(Subject subject);
    Task<bool> SaveChangesAsync();
}
