using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// ExaminerAssignment repository implementation for managing examiner assignments
/// </summary>
public class ExaminerAssignmentRepository : IExaminerAssignmentRepository
{
    private readonly Prn232GradingContext _context;

    public ExaminerAssignmentRepository(Prn232GradingContext context)
    {
        _context = context;
 }

    public async Task<IEnumerable<ExaminerAssignment>> GetAllAsync()
    {
   return await _context.ExaminerAssignments
       .Include(ea => ea.Examiner)
       .Include(ea => ea.Submission)
  .ThenInclude(s => s.Student)
         .Include(ea => ea.Submission)
            .ThenInclude(s => s.Exam)
      .OrderByDescending(ea => ea.AssignedAt)
          .ToListAsync();
  }

    public async Task<ExaminerAssignment?> GetByIdAsync(long assignmentId)
    {
        return await _context.ExaminerAssignments
            .Include(ea => ea.Examiner)
  .Include(ea => ea.Submission)
           .ThenInclude(s => s.Student)
       .Include(ea => ea.Submission)
   .ThenInclude(s => s.Exam)
            .FirstOrDefaultAsync(ea => ea.AssignmentId == assignmentId);
    }

 public async Task<IEnumerable<ExaminerAssignment>> GetBySubmissionIdAsync(long submissionId)
    {
  return await _context.ExaminerAssignments
            .Include(ea => ea.Examiner)
       .Include(ea => ea.AssignedByNavigation)
     .Where(ea => ea.SubmissionId == submissionId)
         .OrderBy(ea => ea.AssignedAt)
    .ToListAsync();
    }

  public async Task<IEnumerable<ExaminerAssignment>> GetByExaminerIdAsync(Guid examinerId)
    {
     return await _context.ExaminerAssignments
       .Include(ea => ea.Submission)
          .ThenInclude(s => s.Student)
      .Include(ea => ea.Submission)
    .ThenInclude(s => s.Exam)
         .Where(ea => ea.ExaminerId == examinerId)
            .OrderByDescending(ea => ea.AssignedAt)
   .ToListAsync();
    }

    public async Task<IEnumerable<ExaminerAssignment>> GetByExamIdAsync(int examId)
    {
    return await _context.ExaminerAssignments
     .Include(ea => ea.Examiner)
 .Include(ea => ea.Submission)
         .ThenInclude(s => s.Student)
         .Where(ea => ea.Submission.ExamId == examId)
 .OrderByDescending(ea => ea.AssignedAt)
            .ToListAsync();
    }

public async Task<ExaminerAssignment?> GetAssignmentWithScoresAsync(long assignmentId)
    {
   return await _context.ExaminerAssignments
            .Include(ea => ea.Examiner)
.Include(ea => ea.Submission)
  .ThenInclude(s => s.Student)
   .Include(ea => ea.Submission)
      .ThenInclude(s => s.Exam)
 .Include(ea => ea.ExaminerScores)
     .ThenInclude(es => es.RubricItem)
   .FirstOrDefaultAsync(ea => ea.AssignmentId == assignmentId);
  }

    public async Task AddAsync(ExaminerAssignment assignment)
    {
        await _context.ExaminerAssignments.AddAsync(assignment);
    }

    public void Update(ExaminerAssignment assignment)
    {
        _context.ExaminerAssignments.Update(assignment);
  }

    public void Delete(ExaminerAssignment assignment)
    {
        _context.ExaminerAssignments.Remove(assignment);
    }

    public async Task<bool> SaveChangesAsync()
 {
        return await _context.SaveChangesAsync() > 0;
    }
}
