using BusinessObjects;

namespace Services;

/// <summary>
/// Semester service interface for business logic
/// </summary>
public interface ISemesterService
{
    Task<IEnumerable<Semester>> GetAllSemestersAsync();
    Task<Semester?> GetSemesterByIdAsync(int semesterId);
    Task<Semester?> GetSemesterByCodeAsync(string code);
    Task<Semester> CreateSemesterAsync(string code, string name);
    Task<Semester> UpdateSemesterAsync(int semesterId, string name);
    Task<bool> DeleteSemesterAsync(int semesterId);
    Task<bool> CodeExistsAsync(string code);
}
