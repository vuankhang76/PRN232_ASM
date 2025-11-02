using BusinessObjects;

namespace Services;

/// <summary>
/// ExaminerAssignment service interface for managing examiner assignments
/// </summary>
public interface IExaminerAssignmentService
{
    Task<IEnumerable<ExaminerAssignment>> GetAllAssignmentsAsync();
    Task<ExaminerAssignment?> GetAssignmentByIdAsync(long assignmentId);
    Task<ExaminerAssignment?> GetAssignmentWithScoresAsync(long assignmentId);
    Task<IEnumerable<ExaminerAssignment>> GetAssignmentsBySubmissionIdAsync(long submissionId);
    Task<IEnumerable<ExaminerAssignment>> GetAssignmentsByExaminerIdAsync(Guid examinerId);
    Task<IEnumerable<ExaminerAssignment>> GetAssignmentsByExamIdAsync(int examId);
    Task<ExaminerAssignment> AssignExaminerAsync(long submissionId, Guid examinerId, Guid? assignedBy = null);
    Task<ExaminerAssignment> UpdateAssignmentStatusAsync(long assignmentId, string status);
    Task<bool> DeleteAssignmentAsync(long assignmentId);
}
