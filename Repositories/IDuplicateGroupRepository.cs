using BusinessObjects;

namespace Repositories;

/// <summary>
/// DuplicateGroup repository interface
/// </summary>
public interface IDuplicateGroupRepository
{
    Task<IEnumerable<DuplicateGroup>> GetAllAsync();
    Task<DuplicateGroup?> GetByIdAsync(long groupId);
    Task<DuplicateGroup?> GetGroupWithMembersAsync(long groupId);
    Task<IEnumerable<DuplicateGroup>> GetByExamIdAsync(int examId);
    Task<IEnumerable<DuplicateGroup>> GetBySimilarityThresholdAsync(int examId, decimal minSimilarity);
    Task AddAsync(DuplicateGroup group);
    void Update(DuplicateGroup group);
    void Delete(DuplicateGroup group);
    Task<bool> SaveChangesAsync();
}
