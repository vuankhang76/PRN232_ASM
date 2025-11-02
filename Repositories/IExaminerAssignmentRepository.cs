using BusinessObjects;

namespace Repositories;

/// <summary>
/// ExaminerAssignment repository interface for managing examiner assignments
/// </summary>
public interface IExaminerAssignmentRepository
{
    Task<IEnumerable<ExaminerAssignment>> GetAllAsync();
  Task<ExaminerAssignment?> GetByIdAsync(long assignmentId);
  Task<IEnumerable<ExaminerAssignment>> GetBySubmissionIdAsync(long submissionId);
    Task<IEnumerable<ExaminerAssignment>> GetByExaminerIdAsync(Guid examinerId);
    Task<IEnumerable<ExaminerAssignment>> GetByExamIdAsync(int examId);
    Task<ExaminerAssignment?> GetAssignmentWithScoresAsync(long assignmentId);
    Task AddAsync(ExaminerAssignment assignment);
    void Update(ExaminerAssignment assignment);
    void Delete(ExaminerAssignment assignment);
    Task<bool> SaveChangesAsync();
}
