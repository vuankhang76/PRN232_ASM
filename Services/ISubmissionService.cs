using BusinessObjects;

namespace Services;

/// <summary>
/// Submission service interface for business logic
/// </summary>
public interface ISubmissionService
{
    Task<IEnumerable<Submission>> GetAllSubmissionsAsync();
    Task<Submission?> GetSubmissionByIdAsync(long submissionId);
    Task<Submission?> GetSubmissionWithDetailsAsync(long submissionId);
    Task<IEnumerable<Submission>> GetSubmissionsByExamAsync(int examId);
    Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId);
    Task<Submission?> GetSubmissionByExamAndStudentAsync(int examId, int studentId);
}
