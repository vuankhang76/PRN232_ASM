using BusinessObjects;

namespace Repositories;

/// <summary>
/// Violation repository interface for managing violations
/// </summary>
public interface IViolationRepository
{
    Task<IEnumerable<Violation>> GetAllAsync();
    Task<Violation?> GetByIdAsync(long violationId);
    Task<IEnumerable<Violation>> GetBySubmissionIdAsync(long submissionId);
  Task<IEnumerable<Violation>> GetByExamIdAsync(int examId);
    Task<IEnumerable<Violation>> GetByViolationTypeIdAsync(int violationTypeId);
 Task AddAsync(Violation violation);
    void Update(Violation violation);
    void Delete(Violation violation);
    Task<bool> SaveChangesAsync();
}
