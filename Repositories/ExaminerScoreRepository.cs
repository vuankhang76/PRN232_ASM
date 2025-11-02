using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// ExaminerScore repository implementation for managing examiner scores
/// </summary>
public class ExaminerScoreRepository : IExaminerScoreRepository
{
    private readonly Prn232GradingContext _context;

    public ExaminerScoreRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExaminerScore>> GetAllAsync()
    {
        return await _context.ExaminerScores
            .Include(es => es.Assignment)
 .ThenInclude(a => a.Examiner)
         .Include(es => es.RubricItem)
     .OrderByDescending(es => es.ScoredAt)
            .ToListAsync();
    }

    public async Task<ExaminerScore?> GetByIdAsync(long examinerScoreId)
    {
 return await _context.ExaminerScores
       .Include(es => es.Assignment)
  .ThenInclude(a => a.Examiner)
   .Include(es => es.RubricItem)
    .FirstOrDefaultAsync(es => es.ExaminerScoreId == examinerScoreId);
    }

  public async Task<IEnumerable<ExaminerScore>> GetByAssignmentIdAsync(long assignmentId)
    {
return await _context.ExaminerScores
       .Include(es => es.RubricItem)
      .Where(es => es.AssignmentId == assignmentId)
       .OrderBy(es => es.RubricItem.OrderNo)
   .ToListAsync();
 }

    public async Task<ExaminerScore?> GetByAssignmentAndRubricAsync(long assignmentId, long rubricItemId)
 {
   return await _context.ExaminerScores
     .Include(es => es.Assignment)
            .Include(es => es.RubricItem)
     .FirstOrDefaultAsync(es => es.AssignmentId == assignmentId && es.RubricItemId == rubricItemId);
    }

    public async Task AddAsync(ExaminerScore score)
    {
 await _context.ExaminerScores.AddAsync(score);
    }

    public void Update(ExaminerScore score)
  {
   _context.ExaminerScores.Update(score);
    }

    public void Delete(ExaminerScore score)
    {
    _context.ExaminerScores.Remove(score);
 }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
