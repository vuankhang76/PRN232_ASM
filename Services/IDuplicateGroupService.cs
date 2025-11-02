using BusinessObjects;

namespace Services;

/// <summary>
/// DuplicateGroup service interface
/// </summary>
public interface IDuplicateGroupService
{
    Task<IEnumerable<DuplicateGroup>> GetAllDuplicateGroupsAsync();
    Task<DuplicateGroup?> GetDuplicateGroupByIdAsync(long groupId);
    Task<DuplicateGroup?> GetGroupWithMembersAsync(long groupId);
    Task<IEnumerable<DuplicateGroup>> GetDuplicateGroupsByExamAsync(int examId);
    Task<IEnumerable<DuplicateGroup>> GetHighSimilarityGroupsAsync(int examId, decimal minSimilarity);
    Task<DuplicateGroup> CreateDuplicateGroupAsync(int examId, decimal similarity, string? reportPath = null);
    Task<bool> DeleteDuplicateGroupAsync(long groupId);
}
