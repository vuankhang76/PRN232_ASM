namespace Repositories;

/// <summary>
/// Unit of Work interface to manage multiple repositories and transactions
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IAppRoleRepository AppRoleRepository { get; }
  IExamRepository ExamRepository { get; }
    ISubmissionRepository SubmissionRepository { get; }
    IStudentRepository StudentRepository { get; }
    ISubjectRepository SubjectRepository { get; }
ISemesterRepository SemesterRepository { get; }
    IRubricItemRepository RubricItemRepository { get; }
    IExaminerAssignmentRepository ExaminerAssignmentRepository { get; }
    IExaminerScoreRepository ExaminerScoreRepository { get; }
    IViolationRepository ViolationRepository { get; }
    IViolationTypeRepository ViolationTypeRepository { get; }
    IIngestBatchRepository IngestBatchRepository { get; }
    IFinalGradeRepository FinalGradeRepository { get; }
    IDetectedStudentFolderRepository DetectedStudentFolderRepository { get; }
    IDuplicateGroupRepository DuplicateGroupRepository { get; }
    IIngestFileRepository IngestFileRepository { get; }
 ISubmissionFileRepository SubmissionFileRepository { get; }
    IExtractedImageRepository ExtractedImageRepository { get; }
    IZeroScoreVerificationRepository ZeroScoreVerificationRepository { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
