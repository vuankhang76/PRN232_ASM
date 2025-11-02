using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// RubricItem repository implementation for managing rubric items
/// </summary>
public class RubricItemRepository : IRubricItemRepository
{
    private readonly Prn232GradingContext _context;

    public RubricItemRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RubricItem>> GetAllAsync()
    {
        return await _context.RubricItems
      .Include(r => r.Exam)
     .OrderBy(r => r.OrderNo)
       .ToListAsync();
    }

    public async Task<RubricItem?> GetByIdAsync(long rubricItemId)
    {
        return await _context.RubricItems
  .Include(r => r.Exam)
       .FirstOrDefaultAsync(r => r.RubricItemId == rubricItemId);
    }

    public async Task<IEnumerable<RubricItem>> GetByExamIdAsync(int examId)
    {
     return await _context.RubricItems
    .Include(r => r.Exam)
     .Where(r => r.ExamId == examId)
       .OrderBy(r => r.OrderNo)
     .ToListAsync();
    }

public async Task<RubricItem?> GetByCodeAsync(string code, int examId)
    {
     return await _context.RubricItems
      .FirstOrDefaultAsync(r => r.Code == code && r.ExamId == examId);
    }

    public async Task AddAsync(RubricItem rubricItem)
    {
        await _context.RubricItems.AddAsync(rubricItem);
    }

    public void Update(RubricItem rubricItem)
    {
     _context.RubricItems.Update(rubricItem);
    }

    public void Delete(RubricItem rubricItem)
  {
     _context.RubricItems.Remove(rubricItem);
    }

    public async Task<bool> SaveChangesAsync()
    {
    return await _context.SaveChangesAsync() > 0;
    }
}
