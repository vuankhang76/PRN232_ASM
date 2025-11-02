using BusinessObjects;

namespace Services;

/// <summary>
/// Student service interface for business logic
/// </summary>
public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int studentId);
    Task<Student?> GetStudentByCodeAsync(string studentCode);
    Task<Student?> GetStudentWithSubmissionsAsync(int studentId);
    Task<Student> CreateStudentAsync(string studentCode, string fullName, string? email = null, string? className = null);
    Task<Student> UpdateStudentAsync(int studentId, string fullName, string? email, string? className);
    Task<bool> DeleteStudentAsync(int studentId);
    Task<bool> StudentCodeExistsAsync(string studentCode);
}
