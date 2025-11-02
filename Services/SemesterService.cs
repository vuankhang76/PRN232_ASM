using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// Semester service implementation for business logic
/// </summary>
public class SemesterService : ISemesterService
{
    private readonly IUnitOfWork _unitOfWork;

    public SemesterService(IUnitOfWork unitOfWork)
    {
    _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Semester>> GetAllSemestersAsync()
  {
        return await _unitOfWork.SemesterRepository.GetAllAsync();
    }

    public async Task<Semester?> GetSemesterByIdAsync(int semesterId)
    {
        return await _unitOfWork.SemesterRepository.GetByIdAsync(semesterId);
    }

    public async Task<Semester?> GetSemesterByCodeAsync(string code)
 {
        return await _unitOfWork.SemesterRepository.GetByCodeAsync(code);
    }

    public async Task<Semester> CreateSemesterAsync(string code, string name)
  {
        // Validate code doesn't exist
        if (await _unitOfWork.SemesterRepository.CodeExistsAsync(code))
        {
   throw new InvalidOperationException($"Semester code '{code}' already exists.");
        }

        var semester = new Semester
        {
         Code = code,
    Name = name
        };

        await _unitOfWork.SemesterRepository.AddAsync(semester);
        await _unitOfWork.SaveChangesAsync();

  return semester;
    }

    public async Task<Semester> UpdateSemesterAsync(int semesterId, string name)
    {
        var semester = await _unitOfWork.SemesterRepository.GetByIdAsync(semesterId);

        if (semester == null)
            throw new InvalidOperationException($"Semester with ID '{semesterId}' not found.");

        semester.Name = name;

 _unitOfWork.SemesterRepository.Update(semester);
        await _unitOfWork.SaveChangesAsync();

        return semester;
    }

    public async Task<bool> DeleteSemesterAsync(int semesterId)
 {
        var semester = await _unitOfWork.SemesterRepository.GetByIdAsync(semesterId);

        if (semester == null)
            return false;

    _unitOfWork.SemesterRepository.Delete(semester);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
        return await _unitOfWork.SemesterRepository.CodeExistsAsync(code);
    }
}
