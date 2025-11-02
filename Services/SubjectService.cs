using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// Subject service implementation for business logic
/// </summary>
public class SubjectService : ISubjectService
{
  private readonly IUnitOfWork _unitOfWork;

    public SubjectService(IUnitOfWork unitOfWork)
    {
_unitOfWork = unitOfWork;
    }

public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
    return await _unitOfWork.SubjectRepository.GetAllAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(int subjectId)
    {
        return await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
  }

    public async Task<Subject?> GetSubjectByCodeAsync(string code)
  {
     return await _unitOfWork.SubjectRepository.GetByCodeAsync(code);
    }

    public async Task<Subject> CreateSubjectAsync(string code, string name)
    {
   // Validate code doesn't exist
if (await _unitOfWork.SubjectRepository.CodeExistsAsync(code))
  {
     throw new InvalidOperationException($"Subject code '{code}' already exists.");
        }

        var subject = new Subject
        {
   Code = code,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.SubjectRepository.AddAsync(subject);
     await _unitOfWork.SaveChangesAsync();

        return subject;
    }

    public async Task<Subject> UpdateSubjectAsync(int subjectId, string name)
    {
 var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);

    if (subject == null)
            throw new InvalidOperationException($"Subject with ID '{subjectId}' not found.");

        subject.Name = name;

        _unitOfWork.SubjectRepository.Update(subject);
        await _unitOfWork.SaveChangesAsync();

        return subject;
    }

    public async Task<bool> DeleteSubjectAsync(int subjectId)
    {
var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);

        if (subject == null)
         return false;

        _unitOfWork.SubjectRepository.Delete(subject);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
      return await _unitOfWork.SubjectRepository.CodeExistsAsync(code);
    }
}
