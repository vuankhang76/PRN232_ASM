using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// FinalGrade repository implementation for managing final grades
/// </summary>
public class FinalGradeRepository : IFinalGradeRepository
{
    private readonly Prn232GradingContext _context;

    public FinalGradeRepository(Prn232GradingContext context)
{
   _context = context;
  }

    public async Task<IEnumerable<FinalGrade>> GetAllAsync()
    {
        return await _context.FinalGrades
.Include(fg => fg.Submission)
  .ThenInclude(s => s.Student)
  .Include(fg => fg.Submission)
       .ThenInclude(s => s.Exam)
    .OrderByDescending(fg => fg.DecidedAt)
     .ToListAsync();
    }

    public async Task<FinalGrade?> GetBySubmissionIdAsync(long submissionId)
    {
        return await _context.FinalGrades
  .Include(fg => fg.Submission)
         .ThenInclude(s => s.Student)
  .Include(fg => fg.Submission)
     .ThenInclude(s => s.Exam)
   .FirstOrDefaultAsync(fg => fg.SubmissionId == submissionId);
    }

    public async Task<IEnumerable<FinalGrade>> GetByExamIdAsync(int examId)
    {
  return await _context.FinalGrades
     .Include(fg => fg.Submission)
    .ThenInclude(s => s.Student)
        .Where(fg => fg.Submission.ExamId == examId)
  .OrderBy(fg => fg.Submission.Student.StudentCode)
    .ToListAsync();
    }

    public async Task AddAsync(FinalGrade finalGrade)
    {
        await _context.FinalGrades.AddAsync(finalGrade);
 }

    public void Update(FinalGrade finalGrade)
    {
     _context.FinalGrades.Update(finalGrade);
    }

    public void Delete(FinalGrade finalGrade)
    {
  _context.FinalGrades.Remove(finalGrade);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
