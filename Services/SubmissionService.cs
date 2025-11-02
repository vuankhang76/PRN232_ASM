using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// Submission service implementation for business logic
/// </summary>
public class SubmissionService : ISubmissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubmissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Submission>> GetAllSubmissionsAsync()
    {
        return await _unitOfWork.SubmissionRepository.GetAllAsync();
    }

    public async Task<Submission?> GetSubmissionByIdAsync(long submissionId)
    {
        return await _unitOfWork.SubmissionRepository.GetByIdAsync(submissionId);
    }

    public async Task<Submission?> GetSubmissionWithDetailsAsync(long submissionId)
    {
        return await _unitOfWork.SubmissionRepository.GetSubmissionWithDetailsAsync(submissionId);
    }

    public async Task<IEnumerable<Submission>> GetSubmissionsByExamAsync(int examId)
    {
        return await _unitOfWork.SubmissionRepository.GetSubmissionsByExamAsync(examId);
    }

    public async Task<IEnumerable<Submission>> GetSubmissionsByStudentAsync(int studentId)
    {
        return await _unitOfWork.SubmissionRepository.GetSubmissionsByStudentAsync(studentId);
    }

    public async Task<Submission?> GetSubmissionByExamAndStudentAsync(int examId, int studentId)
    {
        return await _unitOfWork.SubmissionRepository.GetSubmissionByExamAndStudentAsync(examId, studentId);
    }
}
