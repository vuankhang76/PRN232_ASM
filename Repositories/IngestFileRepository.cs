using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// IngestFile repository implementation
/// </summary>
public class IngestFileRepository : IIngestFileRepository
{
    private readonly Prn232GradingContext _context;

    public IngestFileRepository(Prn232GradingContext context)
    {
        _context = context;
    }

 public async Task<IEnumerable<IngestFile>> GetAllAsync()
    {
     return await _context.IngestFiles
            .Include(f => f.Batch)
    .OrderBy(f => f.RelativePath)
            .ToListAsync();
    }

  public async Task<IngestFile?> GetByIdAsync(long ingestFileId)
    {
        return await _context.IngestFiles
    .Include(f => f.Batch)
 .Include(f => f.SubmissionFiles)
        .FirstOrDefaultAsync(f => f.IngestFileId == ingestFileId);
    }

    public async Task<IEnumerable<IngestFile>> GetByBatchIdAsync(long batchId)
    {
return await _context.IngestFiles
       .Include(f => f.Batch)
.Where(f => f.BatchId == batchId)
  .OrderBy(f => f.RelativePath)
   .ToListAsync();
    }

    public async Task<IngestFile?> GetBySha256Async(byte[] sha256)
    {
        return await _context.IngestFiles
  .Include(f => f.Batch)
      .FirstOrDefaultAsync(f => f.Sha256 == sha256);
    }

    public async Task<IEnumerable<IngestFile>> GetArchiveFilesAsync(long batchId)
    {
 return await _context.IngestFiles
      .Where(f => f.BatchId == batchId && f.IsArchive)
       .OrderBy(f => f.RelativePath)
      .ToListAsync();
    }

    public async Task AddAsync(IngestFile file)
    {
      await _context.IngestFiles.AddAsync(file);
    }

    public void Update(IngestFile file)
    {
  _context.IngestFiles.Update(file);
    }

    public void Delete(IngestFile file)
    {
        _context.IngestFiles.Remove(file);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
