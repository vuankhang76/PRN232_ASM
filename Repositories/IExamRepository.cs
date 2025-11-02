using BusinessObjects;

namespace Repositories;

/// <summary>
/// Exam repository interface for managing Exam entities
/// </summary>
public interface IExamRepository
{
    Task<IEnumerable<Exam>> GetAllAsync();
    Task<Exam?> GetByIdAsync(int examId);
    Task<Exam?> GetExamWithDetailsAsync(int examId);
    Task<IEnumerable<Exam>> GetExamsBySemesterAsync(int semesterId);
    Task<IEnumerable<Exam>> GetExamsBySubjectAsync(int subjectId);
    Task<Exam?> GetByExamCodeAsync(string examCode);
    Task AddAsync(Exam exam);
  void Update(Exam exam);
    void Delete(Exam exam);
    Task<bool> SaveChangesAsync();
}
