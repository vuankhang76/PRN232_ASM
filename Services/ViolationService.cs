using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// Violation service implementation for managing violations
/// </summary>
public class ViolationService : IViolationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ViolationService(IUnitOfWork unitOfWork)
    {
   _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Violation>> GetAllViolationsAsync()
    {
        return await _unitOfWork.ViolationRepository.GetAllAsync();
    }

    public async Task<Violation?> GetViolationByIdAsync(long violationId)
    {
   return await _unitOfWork.ViolationRepository.GetByIdAsync(violationId);
    }

    public async Task<IEnumerable<Violation>> GetViolationsBySubmissionIdAsync(long submissionId)
    {
     return await _unitOfWork.ViolationRepository.GetBySubmissionIdAsync(submissionId);
    }

    public async Task<IEnumerable<Violation>> GetViolationsByExamIdAsync(int examId)
    {
   return await _unitOfWork.ViolationRepository.GetByExamIdAsync(examId);
    }

    public async Task<Violation> CreateViolationAsync(long submissionId, int violationTypeId, string message,
  byte severity = 1, string? evidence = null)
    {
  // Validate submission exists
   var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(submissionId);
  if (submission == null)
    throw new InvalidOperationException($"Submission with ID '{submissionId}' not found.");

  // Validate violation type exists
     var violationType = await _unitOfWork.ViolationTypeRepository.GetByIdAsync(violationTypeId);
    if (violationType == null)
   throw new InvalidOperationException($"ViolationType with ID '{violationTypeId}' not found.");

    var violation = new Violation
  {
   SubmissionId = submissionId,
      ViolationTypeId = violationTypeId,
    Message = message,
  Severity = severity,
            Evidence = evidence,
  CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.ViolationRepository.AddAsync(violation);
        await _unitOfWork.SaveChangesAsync();

  return violation;
    }

    public async Task<bool> DeleteViolationAsync(long violationId)
    {
   var violation = await _unitOfWork.ViolationRepository.GetByIdAsync(violationId);

   if (violation == null)
        return false;

     _unitOfWork.ViolationRepository.Delete(violation);
     return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
