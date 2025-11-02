using BusinessObjects;

namespace Services;

/// <summary>
/// IngestBatch service interface for managing file ingestion batches
/// </summary>
public interface IIngestBatchService
{
    Task<IEnumerable<IngestBatch>> GetAllBatchesAsync();
    Task<IngestBatch?> GetBatchByIdAsync(long batchId);
  Task<IngestBatch?> GetBatchWithFilesAsync(long batchId);
    Task<IEnumerable<IngestBatch>> GetBatchesByExamAsync(int examId);
    Task<IngestBatch> CreateBatchAsync(int examId, string sourceArchivePath, string? extractRootPath = null);
    Task<IngestBatch> UpdateBatchStatusAsync(long batchId, string status, string? message = null);
    Task<bool> DeleteBatchAsync(long batchId);
}
