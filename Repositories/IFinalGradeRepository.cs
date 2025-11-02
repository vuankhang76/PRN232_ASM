using BusinessObjects;

namespace Repositories;

/// <summary>
/// FinalGrade repository interface for managing final grades
/// </summary>
public interface IFinalGradeRepository
{
    Task<IEnumerable<FinalGrade>> GetAllAsync();
    Task<FinalGrade?> GetBySubmissionIdAsync(long submissionId);
    Task<IEnumerable<FinalGrade>> GetByExamIdAsync(int examId);
  Task AddAsync(FinalGrade finalGrade);
    void Update(FinalGrade finalGrade);
    void Delete(FinalGrade finalGrade);
    Task<bool> SaveChangesAsync();
}
