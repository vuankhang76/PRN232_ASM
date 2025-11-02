using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// ExaminerAssignment service implementation for managing examiner assignments
/// </summary>
public class ExaminerAssignmentService : IExaminerAssignmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExaminerAssignmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ExaminerAssignment>> GetAllAssignmentsAsync()
    {
        return await _unitOfWork.ExaminerAssignmentRepository.GetAllAsync();
    }

    public async Task<ExaminerAssignment?> GetAssignmentByIdAsync(long assignmentId)
    {
        return await _unitOfWork.ExaminerAssignmentRepository.GetByIdAsync(assignmentId);
    }

    public async Task<ExaminerAssignment?> GetAssignmentWithScoresAsync(long assignmentId)
    {
        return await _unitOfWork.ExaminerAssignmentRepository.GetAssignmentWithScoresAsync(assignmentId);
    }

  public async Task<IEnumerable<ExaminerAssignment>> GetAssignmentsBySubmissionIdAsync(long submissionId)
    {
  return await _unitOfWork.ExaminerAssignmentRepository.GetBySubmissionIdAsync(submissionId);
    }

    public async Task<IEnumerable<ExaminerAssignment>> GetAssignmentsByExaminerIdAsync(Guid examinerId)
    {
    return await _unitOfWork.ExaminerAssignmentRepository.GetByExaminerIdAsync(examinerId);
    }

  public async Task<IEnumerable<ExaminerAssignment>> GetAssignmentsByExamIdAsync(int examId)
    {
  return await _unitOfWork.ExaminerAssignmentRepository.GetByExamIdAsync(examId);
    }

  public async Task<ExaminerAssignment> AssignExaminerAsync(long submissionId, Guid examinerId, Guid? assignedBy = null)
    {
        // Validate submission exists
        var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(submissionId);
        if (submission == null)
 throw new InvalidOperationException($"Submission with ID '{submissionId}' not found.");

        // Validate examiner exists
        var examiner = await _unitOfWork.UserRepository.GetByIdAsync(examinerId);
        if (examiner == null)
            throw new InvalidOperationException($"Examiner with ID '{examinerId}' not found.");

    var assignment = new ExaminerAssignment
        {
  SubmissionId = submissionId,
 ExaminerId = examinerId,
AssignedBy = assignedBy,
    AssignedAt = DateTime.UtcNow,
     Status = "ASSIGNED"
        };

    await _unitOfWork.ExaminerAssignmentRepository.AddAsync(assignment);
        await _unitOfWork.SaveChangesAsync();

  return assignment;
    }

    public async Task<ExaminerAssignment> UpdateAssignmentStatusAsync(long assignmentId, string status)
    {
      var assignment = await _unitOfWork.ExaminerAssignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
            throw new InvalidOperationException($"Assignment with ID '{assignmentId}' not found.");

        assignment.Status = status;

        _unitOfWork.ExaminerAssignmentRepository.Update(assignment);
        await _unitOfWork.SaveChangesAsync();

        return assignment;
    }

    public async Task<bool> DeleteAssignmentAsync(long assignmentId)
    {
        var assignment = await _unitOfWork.ExaminerAssignmentRepository.GetByIdAsync(assignmentId);

        if (assignment == null)
  return false;

        _unitOfWork.ExaminerAssignmentRepository.Delete(assignment);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
