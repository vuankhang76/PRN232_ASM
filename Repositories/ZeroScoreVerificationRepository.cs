using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// ZeroScoreVerification repository implementation
/// </summary>
public class ZeroScoreVerificationRepository : IZeroScoreVerificationRepository
{
    private readonly Prn232GradingContext _context;

    public ZeroScoreVerificationRepository(Prn232GradingContext context)
    {
   _context = context;
    }

    public async Task<IEnumerable<ZeroScoreVerification>> GetAllAsync()
    {
        return await _context.ZeroScoreVerifications
     .Include(v => v.Submission)
    .ThenInclude(s => s.Student)
   .Include(v => v.Moderator)
   .OrderByDescending(v => v.VerifiedAt)
      .ToListAsync();
    }

    public async Task<ZeroScoreVerification?> GetByIdAsync(long verificationId)
    {
        return await _context.ZeroScoreVerifications
  .Include(v => v.Submission)
  .ThenInclude(s => s.Student)
  .Include(v => v.Moderator)
  .FirstOrDefaultAsync(v => v.VerificationId == verificationId);
    }

    public async Task<ZeroScoreVerification?> GetBySubmissionIdAsync(long submissionId)
    {
   return await _context.ZeroScoreVerifications
     .Include(v => v.Submission)
  .Include(v => v.Moderator)
     .FirstOrDefaultAsync(v => v.SubmissionId == submissionId);
    }

  public async Task<IEnumerable<ZeroScoreVerification>> GetByModeratorIdAsync(Guid moderatorId)
    {
        return await _context.ZeroScoreVerifications
    .Include(v => v.Submission)
    .ThenInclude(s => s.Student)
   .Include(v => v.Moderator)
.Where(v => v.ModeratorId == moderatorId)
  .OrderByDescending(v => v.VerifiedAt)
      .ToListAsync();
    }

    public async Task AddAsync(ZeroScoreVerification verification)
    {
await _context.ZeroScoreVerifications.AddAsync(verification);
    }

    public void Update(ZeroScoreVerification verification)
  {
        _context.ZeroScoreVerifications.Update(verification);
    }

    public void Delete(ZeroScoreVerification verification)
    {
        _context.ZeroScoreVerifications.Remove(verification);
}

    public async Task<bool> SaveChangesAsync()
    {
  return await _context.SaveChangesAsync() > 0;
    }
}
