using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// IngestBatch repository implementation for managing ingest batches
/// </summary>
public class IngestBatchRepository : IIngestBatchRepository
{
    private readonly Prn232GradingContext _context;

    public IngestBatchRepository(Prn232GradingContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<IngestBatch>> GetAllAsync()
    {
    return await _context.IngestBatches
 .Include(b => b.Exam)
  .OrderByDescending(b => b.CreatedAt)
      .ToListAsync();
    }

    public async Task<IngestBatch?> GetByIdAsync(long batchId)
    {
        return await _context.IngestBatches
  .Include(b => b.Exam)
    .FirstOrDefaultAsync(b => b.BatchId == batchId);
    }

    public async Task<IEnumerable<IngestBatch>> GetByExamIdAsync(int examId)
    {
        return await _context.IngestBatches
 .Where(b => b.ExamId == examId)
     .OrderByDescending(b => b.CreatedAt)
       .ToListAsync();
    }

    public async Task<IngestBatch?> GetBatchWithFilesAsync(long batchId)
    {
        return await _context.IngestBatches
       .Include(b => b.Exam)
        .Include(b => b.IngestFiles)
    .Include(b => b.DetectedStudentFolders)
   .FirstOrDefaultAsync(b => b.BatchId == batchId);
    }

    public async Task AddAsync(IngestBatch batch)
 {
   await _context.IngestBatches.AddAsync(batch);
    }

    public void Update(IngestBatch batch)
    {
  _context.IngestBatches.Update(batch);
    }

    public void Delete(IngestBatch batch)
    {
  _context.IngestBatches.Remove(batch);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
