using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Violation repository implementation for managing violations
/// </summary>
public class ViolationRepository : IViolationRepository
{
    private readonly Prn232GradingContext _context;

    public ViolationRepository(Prn232GradingContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Violation>> GetAllAsync()
    {
    return await _context.Violations
     .Include(v => v.Submission)
      .ThenInclude(s => s.Student)
         .Include(v => v.ViolationType)
.OrderByDescending(v => v.CreatedAt)
  .ToListAsync();
    }

 public async Task<Violation?> GetByIdAsync(long violationId)
    {
        return await _context.Violations
   .Include(v => v.Submission)
            .ThenInclude(s => s.Student)
      .Include(v => v.ViolationType)
        .FirstOrDefaultAsync(v => v.ViolationId == violationId);
    }

    public async Task<IEnumerable<Violation>> GetBySubmissionIdAsync(long submissionId)
    {
        return await _context.Violations
       .Include(v => v.ViolationType)
     .Where(v => v.SubmissionId == submissionId)
      .OrderBy(v => v.Severity)
     .ToListAsync();
    }

    public async Task<IEnumerable<Violation>> GetByExamIdAsync(int examId)
    {
        return await _context.Violations
            .Include(v => v.Submission)
      .ThenInclude(s => s.Student)
    .Include(v => v.ViolationType)
       .Where(v => v.Submission.ExamId == examId)
      .OrderByDescending(v => v.CreatedAt)
    .ToListAsync();
    }

    public async Task<IEnumerable<Violation>> GetByViolationTypeIdAsync(int violationTypeId)
    {
   return await _context.Violations
     .Include(v => v.Submission)
  .ThenInclude(s => s.Student)
       .Include(v => v.ViolationType)
  .Where(v => v.ViolationTypeId == violationTypeId)
    .OrderByDescending(v => v.CreatedAt)
       .ToListAsync();
    }

    public async Task AddAsync(Violation violation)
    {
await _context.Violations.AddAsync(violation);
    }

    public void Update(Violation violation)
    {
    _context.Violations.Update(violation);
    }

    public void Delete(Violation violation)
    {
      _context.Violations.Remove(violation);
    }

    public async Task<bool> SaveChangesAsync()
 {
        return await _context.SaveChangesAsync() > 0;
    }
}
