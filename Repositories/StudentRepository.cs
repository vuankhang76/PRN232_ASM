using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Student repository implementation for managing Student entities
/// </summary>
public class StudentRepository : IStudentRepository
{
    private readonly Prn232GradingContext _context;

    public StudentRepository(Prn232GradingContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students
            .OrderBy(s => s.StudentCode)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int studentId)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task<Student?> GetByStudentCodeAsync(string studentCode)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.StudentCode == studentCode);
    }

    public async Task<Student?> GetStudentWithSubmissionsAsync(int studentId)
    {
        return await _context.Students
     .Include(s => s.Submissions)
.ThenInclude(sub => sub.Exam)
    .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task<bool> StudentCodeExistsAsync(string studentCode)
    {
   return await _context.Students
  .AnyAsync(s => s.StudentCode == studentCode);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

 public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public void Delete(Student student)
    {
   _context.Students.Remove(student);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
