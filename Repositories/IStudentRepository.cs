using BusinessObjects;

namespace Repositories;

/// <summary>
/// Student repository interface for managing Student entities
/// </summary>
public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int studentId);
    Task<Student?> GetByStudentCodeAsync(string studentCode);
    Task<Student?> GetStudentWithSubmissionsAsync(int studentId);
    Task<bool> StudentCodeExistsAsync(string studentCode);
    Task AddAsync(Student student);
    void Update(Student student);
    void Delete(Student student);
    Task<bool> SaveChangesAsync();
}
