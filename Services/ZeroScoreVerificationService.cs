using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// ZeroScoreVerification service implementation
/// </summary>
public class ZeroScoreVerificationService : IZeroScoreVerificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ZeroScoreVerificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ZeroScoreVerification>> GetAllVerificationsAsync()
    {
        return await _unitOfWork.ZeroScoreVerificationRepository.GetAllAsync();
    }

    public async Task<ZeroScoreVerification?> GetVerificationByIdAsync(long verificationId)
    {
   return await _unitOfWork.ZeroScoreVerificationRepository.GetByIdAsync(verificationId);
    }

    public async Task<ZeroScoreVerification?> GetVerificationBySubmissionAsync(long submissionId)
    {
     return await _unitOfWork.ZeroScoreVerificationRepository.GetBySubmissionIdAsync(submissionId);
    }

public async Task<IEnumerable<ZeroScoreVerification>> GetVerificationsByModeratorAsync(Guid moderatorId)
    {
   return await _unitOfWork.ZeroScoreVerificationRepository.GetByModeratorIdAsync(moderatorId);
  }

    public async Task<ZeroScoreVerification> CreateVerificationAsync(long submissionId, Guid moderatorId, string? reason = null)
    {
        // Validate submission exists and is zero score
        var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(submissionId);
  if (submission == null)
     throw new InvalidOperationException($"Submission with ID '{submissionId}' not found.");

   if (!submission.IsZeroScore)
       throw new InvalidOperationException("Submission is not marked as zero score.");

  // Validate moderator exists
   var moderator = await _unitOfWork.UserRepository.GetByIdAsync(moderatorId);
 if (moderator == null)
            throw new InvalidOperationException($"Moderator with ID '{moderatorId}' not found.");

        // Check if already verified
        var existing = await _unitOfWork.ZeroScoreVerificationRepository.GetBySubmissionIdAsync(submissionId);
        if (existing != null)
  throw new InvalidOperationException("This submission has already been verified.");

 var verification = new ZeroScoreVerification
        {
   SubmissionId = submissionId,
         ModeratorId = moderatorId,
     Reason = reason,
 VerifiedAt = DateTime.UtcNow
        };

   await _unitOfWork.ZeroScoreVerificationRepository.AddAsync(verification);
        await _unitOfWork.SaveChangesAsync();

    return verification;
 }

    public async Task<bool> DeleteVerificationAsync(long verificationId)
    {
 var verification = await _unitOfWork.ZeroScoreVerificationRepository.GetByIdAsync(verificationId);

        if (verification == null)
            return false;

        _unitOfWork.ZeroScoreVerificationRepository.Delete(verification);
   return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
