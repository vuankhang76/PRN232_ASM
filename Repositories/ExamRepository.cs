using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Exam repository implementation for managing Exam entities
/// </summary>
public class ExamRepository : IExamRepository
{
 private readonly Prn232GradingContext _context;

    public ExamRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Exam>> GetAllAsync()
    {
        return await _context.Exams
   .Include(e => e.Subject)
            .Include(e => e.Semester)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<Exam?> GetByIdAsync(int examId)
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .Include(e => e.Semester)
            .FirstOrDefaultAsync(e => e.ExamId == examId);
    }

    public async Task<Exam?> GetExamWithDetailsAsync(int examId)
{
        return await _context.Exams
     .Include(e => e.Subject)
            .Include(e => e.Semester)
            .Include(e => e.RubricItems)
     .Include(e => e.Submissions)
   .FirstOrDefaultAsync(e => e.ExamId == examId);
    }

    public async Task<IEnumerable<Exam>> GetExamsBySemesterAsync(int semesterId)
    {
      return await _context.Exams
 .Include(e => e.Subject)
       .Include(e => e.Semester)
      .Where(e => e.SemesterId == semesterId)
        .OrderBy(e => e.ExamName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetExamsBySubjectAsync(int subjectId)
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .Include(e => e.Semester)
        .Where(e => e.SubjectId == subjectId)
       .OrderByDescending(e => e.CreatedAt)
  .ToListAsync();
    }

    public async Task<Exam?> GetByExamCodeAsync(string examCode)
    {
        return await _context.Exams
            .Include(e => e.Subject)
.Include(e => e.Semester)
          .FirstOrDefaultAsync(e => e.ExamCode == examCode);
    }

    public async Task AddAsync(Exam exam)
    {
  await _context.Exams.AddAsync(exam);
    }

    public void Update(Exam exam)
    {
        _context.Exams.Update(exam);
    }

    public void Delete(Exam exam)
  {
  _context.Exams.Remove(exam);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
}
