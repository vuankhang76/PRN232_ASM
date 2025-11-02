using BusinessObjects;

namespace Services;

/// <summary>
/// ExaminerScore service interface for managing examiner scores
/// </summary>
public interface IExaminerScoreService
{
    Task<IEnumerable<ExaminerScore>> GetAllScoresAsync();
    Task<ExaminerScore?> GetScoreByIdAsync(long examinerScoreId);
    Task<IEnumerable<ExaminerScore>> GetScoresByAssignmentIdAsync(long assignmentId);
    Task<ExaminerScore> SubmitScoreAsync(long assignmentId, int rubricItemId, decimal score, string? comment = null);
    Task<ExaminerScore> UpdateScoreAsync(long examinerScoreId, decimal score, string? comment = null);
    Task<bool> DeleteScoreAsync(long examinerScoreId);
    Task<decimal> CalculateTotalScoreForAssignmentAsync(long assignmentId);
}
