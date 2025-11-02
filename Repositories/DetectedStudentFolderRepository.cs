using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// DetectedStudentFolder repository implementation
/// </summary>
public class DetectedStudentFolderRepository : IDetectedStudentFolderRepository
{
  private readonly Prn232GradingContext _context;

    public DetectedStudentFolderRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DetectedStudentFolder>> GetAllAsync()
    {
        return await _context.DetectedStudentFolders
            .Include(f => f.Batch)
         .OrderBy(f => f.StudentCode)
            .ToListAsync();
    }

 public async Task<DetectedStudentFolder?> GetByIdAsync(long folderId)
    {
        return await _context.DetectedStudentFolders
 .Include(f => f.Batch)
      .Include(f => f.Submissions)
            .FirstOrDefaultAsync(f => f.FolderId == folderId);
    }

    public async Task<IEnumerable<DetectedStudentFolder>> GetByBatchIdAsync(long batchId)
    {
     return await _context.DetectedStudentFolders
       .Include(f => f.Batch)
    .Where(f => f.BatchId == batchId)
 .OrderBy(f => f.StudentCode)
     .ToListAsync();
 }

    public async Task<DetectedStudentFolder?> GetByStudentCodeAsync(long batchId, string studentCode)
    {
        return await _context.DetectedStudentFolders
            .Include(f => f.Batch)
   .FirstOrDefaultAsync(f => f.BatchId == batchId && f.StudentCode == studentCode);
    }

    public async Task AddAsync(DetectedStudentFolder folder)
 {
        await _context.DetectedStudentFolders.AddAsync(folder);
    }

    public void Update(DetectedStudentFolder folder)
    {
        _context.DetectedStudentFolders.Update(folder);
    }

    public void Delete(DetectedStudentFolder folder)
    {
        _context.DetectedStudentFolders.Remove(folder);
    }

    public async Task<bool> SaveChangesAsync()
    {
  return await _context.SaveChangesAsync() > 0;
    }
}
