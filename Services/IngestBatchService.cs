using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// IngestBatch service implementation
/// </summary>
public class IngestBatchService : IIngestBatchService
{
    private readonly IUnitOfWork _unitOfWork;

    public IngestBatchService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<IngestBatch>> GetAllBatchesAsync()
    {
   return await _unitOfWork.IngestBatchRepository.GetAllAsync();
    }

    public async Task<IngestBatch?> GetBatchByIdAsync(long batchId)
    {
 return await _unitOfWork.IngestBatchRepository.GetByIdAsync(batchId);
    }

    public async Task<IngestBatch?> GetBatchWithFilesAsync(long batchId)
    {
    return await _unitOfWork.IngestBatchRepository.GetBatchWithFilesAsync(batchId);
    }

    public async Task<IEnumerable<IngestBatch>> GetBatchesByExamAsync(int examId)
{
        return await _unitOfWork.IngestBatchRepository.GetByExamIdAsync(examId);
    }

    public async Task<IngestBatch> CreateBatchAsync(int examId, string sourceArchivePath, string? extractRootPath = null)
    {
        // Validate exam exists
      var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
        if (exam == null)
    throw new InvalidOperationException($"Exam with ID '{examId}' not found.");

        var batch = new IngestBatch
      {
            ExamId = examId,
        SourceArchivePath = sourceArchivePath,
       ExtractRootPath = extractRootPath,
       Status = "UPLOADED",
 CreatedAt = DateTime.UtcNow
 };

        await _unitOfWork.IngestBatchRepository.AddAsync(batch);
  await _unitOfWork.SaveChangesAsync();

   return batch;
    }

    public async Task<IngestBatch> UpdateBatchStatusAsync(long batchId, string status, string? message = null)
    {
     var batch = await _unitOfWork.IngestBatchRepository.GetByIdAsync(batchId);

     if (batch == null)
       throw new InvalidOperationException($"Batch with ID '{batchId}' not found.");

        batch.Status = status;
        batch.Message = message;

        _unitOfWork.IngestBatchRepository.Update(batch);
        await _unitOfWork.SaveChangesAsync();

        return batch;
    }

    public async Task<bool> DeleteBatchAsync(long batchId)
    {
   var batch = await _unitOfWork.IngestBatchRepository.GetByIdAsync(batchId);

     if (batch == null)
        return false;

    _unitOfWork.IngestBatchRepository.Delete(batch);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
