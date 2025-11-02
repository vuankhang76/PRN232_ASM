using BusinessObjects;

namespace Repositories;

/// <summary>
/// ExaminerScore repository interface for managing examiner scores
/// </summary>
public interface IExaminerScoreRepository
{
    Task<IEnumerable<ExaminerScore>> GetAllAsync();
    Task<ExaminerScore?> GetByIdAsync(long examinerScoreId);
    Task<IEnumerable<ExaminerScore>> GetByAssignmentIdAsync(long assignmentId);
    Task<ExaminerScore?> GetByAssignmentAndRubricAsync(long assignmentId, long rubricItemId);
    Task AddAsync(ExaminerScore score);
    void Update(ExaminerScore score);
    void Delete(ExaminerScore score);
  Task<bool> SaveChangesAsync();
}
