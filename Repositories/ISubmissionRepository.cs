using BusinessObjects;

namespace Repositories;

/// <summary>
/// Submission repository interface for managing Submission entities
/// </summary>
public interface ISubmissionRepository
{
    Task<IEnumerable<Submission>> GetAllAsync();
    Task<Submission?> GetByIdAsync(long submissionId);
    Task<Submission?> GetSubmissionWithDetailsAsync(long submissionId);
  Task<IEnumerable<Submission>> GetSubmissionsByExamAsync(int examId);
    Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId);
    Task<Submission?> GetSubmissionByExamAndStudentAsync(int examId, int studentId);
    Task AddAsync(Submission submission);
  void Update(Submission submission);
    void Delete(Submission submission);
    Task<bool> SaveChangesAsync();
}
