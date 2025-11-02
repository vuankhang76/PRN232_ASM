using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// SubmissionFile repository implementation
/// </summary>
public class SubmissionFileRepository : ISubmissionFileRepository
{
    private readonly Prn232GradingContext _context;

 public SubmissionFileRepository(Prn232GradingContext context)
    {
    _context = context;
    }

 public async Task<IEnumerable<SubmissionFile>> GetAllAsync()
    {
    return await _context.SubmissionFiles
   .Include(sf => sf.Submission)
       .ThenInclude(s => s.Student)
      .Include(sf => sf.IngestFile)
            .ToListAsync();
  }

    public async Task<SubmissionFile?> GetByIdAsync(long submissionFileId)
    {
 return await _context.SubmissionFiles
     .Include(sf => sf.Submission)
     .Include(sf => sf.IngestFile)
       .FirstOrDefaultAsync(sf => sf.SubmissionFileId == submissionFileId);
  }

public async Task<IEnumerable<SubmissionFile>> GetBySubmissionIdAsync(long submissionId)
    {
        return await _context.SubmissionFiles
    .Include(sf => sf.IngestFile)
 .Where(sf => sf.SubmissionId == submissionId)
      .ToListAsync();
    }

    public async Task<IEnumerable<SubmissionFile>> GetByIngestFileIdAsync(long ingestFileId)
    {
        return await _context.SubmissionFiles
   .Include(sf => sf.Submission)
         .ThenInclude(s => s.Student)
  .Where(sf => sf.IngestFileId == ingestFileId)
       .ToListAsync();
    }

    public async Task AddAsync(SubmissionFile file)
    {
   await _context.SubmissionFiles.AddAsync(file);
    }

    public void Update(SubmissionFile file)
    {
  _context.SubmissionFiles.Update(file);
    }

    public void Delete(SubmissionFile file)
  {
  _context.SubmissionFiles.Remove(file);
    }

  public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
