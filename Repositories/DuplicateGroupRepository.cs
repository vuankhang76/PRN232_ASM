using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// DuplicateGroup repository implementation
/// </summary>
public class DuplicateGroupRepository : IDuplicateGroupRepository
{
    private readonly Prn232GradingContext _context;

    public DuplicateGroupRepository(Prn232GradingContext context)
    {
 _context = context;
    }

    public async Task<IEnumerable<DuplicateGroup>> GetAllAsync()
    {
        return await _context.DuplicateGroups
      .Include(g => g.Exam)
     .OrderByDescending(g => g.Similarity)
       .ToListAsync();
    }

    public async Task<DuplicateGroup?> GetByIdAsync(long groupId)
    {
        return await _context.DuplicateGroups
    .Include(g => g.Exam)
       .FirstOrDefaultAsync(g => g.GroupId == groupId);
    }

    public async Task<DuplicateGroup?> GetGroupWithMembersAsync(long groupId)
    {
        return await _context.DuplicateGroups
            .Include(g => g.Exam)
      .Include(g => g.DuplicateGroupMembers)
      .ThenInclude(m => m.Submission)
      .ThenInclude(s => s.Student)
            .FirstOrDefaultAsync(g => g.GroupId == groupId);
    }

 public async Task<IEnumerable<DuplicateGroup>> GetByExamIdAsync(int examId)
 {
        return await _context.DuplicateGroups
   .Include(g => g.Exam)
            .Include(g => g.DuplicateGroupMembers)
  .Where(g => g.ExamId == examId)
            .OrderByDescending(g => g.Similarity)
            .ToListAsync();
    }

    public async Task<IEnumerable<DuplicateGroup>> GetBySimilarityThresholdAsync(int examId, decimal minSimilarity)
    {
  return await _context.DuplicateGroups
            .Include(g => g.Exam)
   .Include(g => g.DuplicateGroupMembers)
         .ThenInclude(m => m.Submission)
  .ThenInclude(s => s.Student)
  .Where(g => g.ExamId == examId && g.Similarity >= minSimilarity)
    .OrderByDescending(g => g.Similarity)
            .ToListAsync();
    }

    public async Task AddAsync(DuplicateGroup group)
    {
        await _context.DuplicateGroups.AddAsync(group);
    }

    public void Update(DuplicateGroup group)
    {
  _context.DuplicateGroups.Update(group);
    }

    public void Delete(DuplicateGroup group)
    {
        _context.DuplicateGroups.Remove(group);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
  }
}
