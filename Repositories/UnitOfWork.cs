using BusinessObjects;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repositories;

/// <summary>
/// Unit of Work implementation to manage multiple repositories and transactions
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly Prn232GradingContext _context;
    private IDbContextTransaction? _transaction;

    public IUserRepository UserRepository { get; }
    public IAppRoleRepository AppRoleRepository { get; }
    public IExamRepository ExamRepository { get; }
    public ISubmissionRepository SubmissionRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public ISubjectRepository SubjectRepository { get; }
    public ISemesterRepository SemesterRepository { get; }
    public IRubricItemRepository RubricItemRepository { get; }
    public IExaminerAssignmentRepository ExaminerAssignmentRepository { get; }
    public IExaminerScoreRepository ExaminerScoreRepository { get; }
    public IViolationRepository ViolationRepository { get; }
    public IViolationTypeRepository ViolationTypeRepository { get; }
    public IIngestBatchRepository IngestBatchRepository { get; }
    public IFinalGradeRepository FinalGradeRepository { get; }
    public IDetectedStudentFolderRepository DetectedStudentFolderRepository { get; }
    public IDuplicateGroupRepository DuplicateGroupRepository { get; }
    public IIngestFileRepository IngestFileRepository { get; }
    public ISubmissionFileRepository SubmissionFileRepository { get; }
    public IExtractedImageRepository ExtractedImageRepository { get; }
    public IZeroScoreVerificationRepository ZeroScoreVerificationRepository { get; }

    public UnitOfWork(Prn232GradingContext context)
    {
        _context = context;
     
        // Initialize all repositories
        UserRepository = new UserRepository(_context);
        AppRoleRepository = new AppRoleRepository(_context);
        ExamRepository = new ExamRepository(_context);
        SubmissionRepository = new SubmissionRepository(_context);
        StudentRepository = new StudentRepository(_context);
        SubjectRepository = new SubjectRepository(_context);
        SemesterRepository = new SemesterRepository(_context);
        RubricItemRepository = new RubricItemRepository(_context);
        ExaminerAssignmentRepository = new ExaminerAssignmentRepository(_context);
        ExaminerScoreRepository = new ExaminerScoreRepository(_context);
      ViolationRepository = new ViolationRepository(_context);
    ViolationTypeRepository = new ViolationTypeRepository(_context);
 IngestBatchRepository = new IngestBatchRepository(_context);
     FinalGradeRepository = new FinalGradeRepository(_context);
        DetectedStudentFolderRepository = new DetectedStudentFolderRepository(_context);
        DuplicateGroupRepository = new DuplicateGroupRepository(_context);
        IngestFileRepository = new IngestFileRepository(_context);
        SubmissionFileRepository = new SubmissionFileRepository(_context);
        ExtractedImageRepository = new ExtractedImageRepository(_context);
    ZeroScoreVerificationRepository = new ZeroScoreVerificationRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
{
     _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
    try
      {
        await _context.SaveChangesAsync();
 if (_transaction != null)
     {
    await _transaction.CommitAsync();
  }
        }
     catch
    {
         await RollbackTransactionAsync();
         throw;
        }
        finally
        {
  if (_transaction != null)
     {
        await _transaction.DisposeAsync();
  _transaction = null;
            }
        }
    }

  public async Task RollbackTransactionAsync()
  {
  if (_transaction != null)
      {
          await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
    _transaction = null;
}
    }

    public void Dispose()
    {
        _transaction?.Dispose();
  _context.Dispose();
    }
}
