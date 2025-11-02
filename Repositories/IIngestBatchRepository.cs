using BusinessObjects;

namespace Repositories;

/// <summary>
/// IngestBatch repository interface for managing ingest batches
/// </summary>
public interface IIngestBatchRepository
{
Task<IEnumerable<IngestBatch>> GetAllAsync();
    Task<IngestBatch?> GetByIdAsync(long batchId);
    Task<IEnumerable<IngestBatch>> GetByExamIdAsync(int examId);
    Task<IngestBatch?> GetBatchWithFilesAsync(long batchId);
    Task AddAsync(IngestBatch batch);
    void Update(IngestBatch batch);
    void Delete(IngestBatch batch);
    Task<bool> SaveChangesAsync();
}
