using BusinessObjects;

namespace Repositories;

/// <summary>
/// DetectedStudentFolder repository interface
/// </summary>
public interface IDetectedStudentFolderRepository
{
Task<IEnumerable<DetectedStudentFolder>> GetAllAsync();
    Task<DetectedStudentFolder?> GetByIdAsync(long folderId);
    Task<IEnumerable<DetectedStudentFolder>> GetByBatchIdAsync(long batchId);
    Task<DetectedStudentFolder?> GetByStudentCodeAsync(long batchId, string studentCode);
    Task AddAsync(DetectedStudentFolder folder);
    void Update(DetectedStudentFolder folder);
    void Delete(DetectedStudentFolder folder);
    Task<bool> SaveChangesAsync();
}
