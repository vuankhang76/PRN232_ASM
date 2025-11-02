using BusinessObjects;

namespace Repositories;

/// <summary>
/// ZeroScoreVerification repository interface
/// </summary>
public interface IZeroScoreVerificationRepository
{
    Task<IEnumerable<ZeroScoreVerification>> GetAllAsync();
Task<ZeroScoreVerification?> GetByIdAsync(long verificationId);
Task<ZeroScoreVerification?> GetBySubmissionIdAsync(long submissionId);
    Task<IEnumerable<ZeroScoreVerification>> GetByModeratorIdAsync(Guid moderatorId);
    Task AddAsync(ZeroScoreVerification verification);
    void Update(ZeroScoreVerification verification);
    void Delete(ZeroScoreVerification verification);
    Task<bool> SaveChangesAsync();
}
