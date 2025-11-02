using BusinessObjects;

namespace Services;

/// <summary>
/// Violation service interface for managing violations
/// </summary>
public interface IViolationService
{
  Task<IEnumerable<Violation>> GetAllViolationsAsync();
  Task<Violation?> GetViolationByIdAsync(long violationId);
    Task<IEnumerable<Violation>> GetViolationsBySubmissionIdAsync(long submissionId);
Task<IEnumerable<Violation>> GetViolationsByExamIdAsync(int examId);
  Task<Violation> CreateViolationAsync(long submissionId, int violationTypeId, string message, 
        byte severity = 1, string? evidence = null);
    Task<bool> DeleteViolationAsync(long violationId);
}
