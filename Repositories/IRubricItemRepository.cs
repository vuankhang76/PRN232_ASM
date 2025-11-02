using BusinessObjects;

namespace Repositories;

/// <summary>
/// RubricItem repository interface for managing rubric items
/// </summary>
public interface IRubricItemRepository
{
    Task<IEnumerable<RubricItem>> GetAllAsync();
    Task<RubricItem?> GetByIdAsync(long rubricItemId);
    Task<IEnumerable<RubricItem>> GetByExamIdAsync(int examId);
    Task<RubricItem?> GetByCodeAsync(string code, int examId);
    Task AddAsync(RubricItem rubricItem);
    void Update(RubricItem rubricItem);
    void Delete(RubricItem rubricItem);
 Task<bool> SaveChangesAsync();
}
