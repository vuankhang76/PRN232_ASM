using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Submission repository implementation for managing Submission entities
/// </summary>
public class SubmissionRepository : ISubmissionRepository
{
    private readonly Prn232GradingContext _context;

    public SubmissionRepository(Prn232GradingContext context)
    {
   _context = context;
    }

    public async Task<IEnumerable<Submission>> GetAllAsync()
    {
        return await _context.Submissions
    .Include(s => s.Student)
            .Include(s => s.Exam)
            .OrderByDescending(s => s.UploadedAt)
            .ToListAsync();
    }

    public async Task<Submission?> GetByIdAsync(long submissionId)
    {
        return await _context.Submissions
            .Include(s => s.Student)
  .Include(s => s.Exam)
   .FirstOrDefaultAsync(s => s.SubmissionId == submissionId);
 }

    public async Task<Submission?> GetSubmissionWithDetailsAsync(long submissionId)
    {
   return await _context.Submissions
       .Include(s => s.Student)
            .Include(s => s.Exam)
       .ThenInclude(e => e.Subject)
    .Include(s => s.Exam)
 .ThenInclude(e => e.Semester)
  .Include(s => s.SubmissionFiles)
       .ThenInclude(sf => sf.IngestFile)
    .Include(s => s.ExaminerAssignments)
             .ThenInclude(ea => ea.Examiner)
  .Include(s => s.FinalGrade)
   .FirstOrDefaultAsync(s => s.SubmissionId == submissionId);
    }

    public async Task<IEnumerable<Submission>> GetSubmissionsByExamAsync(int examId)
    {
  return await _context.Submissions
  .Include(s => s.Student)
            .Include(s => s.Exam)
   .Where(s => s.ExamId == examId)
     .OrderBy(s => s.Student.StudentCode)
       .ToListAsync();
    }

    public async Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId)
    {
 return await _context.Submissions
       .Include(s => s.Student)
.Include(s => s.Exam)
            .ThenInclude(e => e.Subject)
          .Where(s => s.StudentId == studentId)
            .OrderByDescending(s => s.UploadedAt)
     .ToListAsync();
    }

    public async Task<Submission?> GetSubmissionByExamAndStudentAsync(int examId, int studentId)
    {
        return await _context.Submissions
   .Include(s => s.Student)
            .Include(s => s.Exam)
     .FirstOrDefaultAsync(s => s.ExamId == examId && s.StudentId == studentId);
    }

    public async Task AddAsync(Submission submission)
 {
    await _context.Submissions.AddAsync(submission);
    }

    public void Update(Submission submission)
    {
 _context.Submissions.Update(submission);
    }

    public void Delete(Submission submission)
 {
        _context.Submissions.Remove(submission);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
