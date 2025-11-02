using BusinessObjects;

namespace Repositories;

/// <summary>
/// SubmissionFile repository interface
/// </summary>
public interface ISubmissionFileRepository
{
    Task<IEnumerable<SubmissionFile>> GetAllAsync();
    Task<SubmissionFile?> GetByIdAsync(long submissionFileId);
    Task<IEnumerable<SubmissionFile>> GetBySubmissionIdAsync(long submissionId);
    Task<IEnumerable<SubmissionFile>> GetByIngestFileIdAsync(long ingestFileId);
    Task AddAsync(SubmissionFile file);
    void Update(SubmissionFile file);
    void Delete(SubmissionFile file);
  Task<bool> SaveChangesAsync();
}
