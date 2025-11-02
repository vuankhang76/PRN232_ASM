using BusinessObjects;

namespace Services;

/// <summary>
/// ZeroScoreVerification service interface for moderators
/// </summary>
public interface IZeroScoreVerificationService
{
  Task<IEnumerable<ZeroScoreVerification>> GetAllVerificationsAsync();
    Task<ZeroScoreVerification?> GetVerificationByIdAsync(long verificationId);
    Task<ZeroScoreVerification?> GetVerificationBySubmissionAsync(long submissionId);
    Task<IEnumerable<ZeroScoreVerification>> GetVerificationsByModeratorAsync(Guid moderatorId);
    Task<ZeroScoreVerification> CreateVerificationAsync(long submissionId, Guid moderatorId, string? reason = null);
    Task<bool> DeleteVerificationAsync(long verificationId);
}
