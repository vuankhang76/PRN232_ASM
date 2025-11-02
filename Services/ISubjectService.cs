using BusinessObjects;

namespace Services;

/// <summary>
/// Subject service interface for business logic
/// </summary>
public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    Task<Subject?> GetSubjectByIdAsync(int subjectId);
    Task<Subject?> GetSubjectByCodeAsync(string code);
    Task<Subject> CreateSubjectAsync(string code, string name);
    Task<Subject> UpdateSubjectAsync(int subjectId, string name);
    Task<bool> DeleteSubjectAsync(int subjectId);
    Task<bool> CodeExistsAsync(string code);
}
