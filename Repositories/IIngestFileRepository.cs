using BusinessObjects;

namespace Repositories;

/// <summary>
/// IngestFile repository interface
/// </summary>
public interface IIngestFileRepository
{
    Task<IEnumerable<IngestFile>> GetAllAsync();
    Task<IngestFile?> GetByIdAsync(long ingestFileId);
    Task<IEnumerable<IngestFile>> GetByBatchIdAsync(long batchId);
    Task<IngestFile?> GetBySha256Async(byte[] sha256);
    Task<IEnumerable<IngestFile>> GetArchiveFilesAsync(long batchId);
    Task AddAsync(IngestFile file);
    void Update(IngestFile file);
    void Delete(IngestFile file);
    Task<bool> SaveChangesAsync();
}
