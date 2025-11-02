using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// Student service implementation for business logic
/// </summary>
public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _unitOfWork.StudentRepository.GetAllAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
    }

    public async Task<Student?> GetStudentByCodeAsync(string studentCode)
    {
  return await _unitOfWork.StudentRepository.GetByStudentCodeAsync(studentCode);
    }

    public async Task<Student?> GetStudentWithSubmissionsAsync(int studentId)
    {
        return await _unitOfWork.StudentRepository.GetStudentWithSubmissionsAsync(studentId);
    }

    public async Task<Student> CreateStudentAsync(string studentCode, string fullName, string? email = null, string? className = null)
    {
        // Validate student code doesn't exist
   if (await _unitOfWork.StudentRepository.StudentCodeExistsAsync(studentCode))
    {
        throw new InvalidOperationException($"Student code '{studentCode}' already exists.");
        }

   var student = new Student
        {
 StudentCode = studentCode,
            FullName = fullName,
     Email = email,
        ClassName = className,
      CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.StudentRepository.AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

     return student;
    }

  public async Task<Student> UpdateStudentAsync(int studentId, string fullName, string? email, string? className)
    {
        var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);

        if (student == null)
            throw new InvalidOperationException($"Student with ID '{studentId}' not found.");

        student.FullName = fullName;
 student.Email = email;
student.ClassName = className;

        _unitOfWork.StudentRepository.Update(student);
        await _unitOfWork.SaveChangesAsync();

        return student;
    }

    public async Task<bool> DeleteStudentAsync(int studentId)
    {
        var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);

        if (student == null)
          return false;

        _unitOfWork.StudentRepository.Delete(student);
     return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> StudentCodeExistsAsync(string studentCode)
  {
        return await _unitOfWork.StudentRepository.StudentCodeExistsAsync(studentCode);
    }
}
