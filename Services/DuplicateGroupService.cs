using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// DuplicateGroup service implementation
/// </summary>
public class DuplicateGroupService : IDuplicateGroupService
{
    private readonly IUnitOfWork _unitOfWork;

    public DuplicateGroupService(IUnitOfWork unitOfWork)
    {
    _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DuplicateGroup>> GetAllDuplicateGroupsAsync()
    {
        return await _unitOfWork.DuplicateGroupRepository.GetAllAsync();
    }

    public async Task<DuplicateGroup?> GetDuplicateGroupByIdAsync(long groupId)
    {
return await _unitOfWork.DuplicateGroupRepository.GetByIdAsync(groupId);
    }

    public async Task<DuplicateGroup?> GetGroupWithMembersAsync(long groupId)
    {
   return await _unitOfWork.DuplicateGroupRepository.GetGroupWithMembersAsync(groupId);
  }

    public async Task<IEnumerable<DuplicateGroup>> GetDuplicateGroupsByExamAsync(int examId)
    {
return await _unitOfWork.DuplicateGroupRepository.GetByExamIdAsync(examId);
    }

    public async Task<IEnumerable<DuplicateGroup>> GetHighSimilarityGroupsAsync(int examId, decimal minSimilarity)
{
   return await _unitOfWork.DuplicateGroupRepository.GetBySimilarityThresholdAsync(examId, minSimilarity);
    }

 public async Task<DuplicateGroup> CreateDuplicateGroupAsync(int examId, decimal similarity, string? reportPath = null)
    {
        // Validate exam exists
var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
        if (exam == null)
    throw new InvalidOperationException($"Exam with ID '{examId}' not found.");

        var group = new DuplicateGroup
        {
       ExamId = examId,
       Similarity = similarity,
   ReportPath = reportPath,
      CreatedAt = DateTime.UtcNow
};

     await _unitOfWork.DuplicateGroupRepository.AddAsync(group);
     await _unitOfWork.SaveChangesAsync();

        return group;
  }

    public async Task<bool> DeleteDuplicateGroupAsync(long groupId)
    {
   var group = await _unitOfWork.DuplicateGroupRepository.GetByIdAsync(groupId);

   if (group == null)
            return false;

  _unitOfWork.DuplicateGroupRepository.Delete(group);
 return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
