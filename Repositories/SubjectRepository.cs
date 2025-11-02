using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Subject repository implementation for managing Subject entities
/// </summary>
public class SubjectRepository : ISubjectRepository
{
    private readonly Prn232GradingContext _context;

    public SubjectRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Subject>> GetAllAsync()
    {
 return await _context.Subjects
            .OrderBy(s => s.Code)
   .ToListAsync();
    }

    public async Task<Subject?> GetByIdAsync(int subjectId)
    {
        return await _context.Subjects
       .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
    }

    public async Task<Subject?> GetByCodeAsync(string code)
    {
        return await _context.Subjects
   .FirstOrDefaultAsync(s => s.Code == code);
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
        return await _context.Subjects
        .AnyAsync(s => s.Code == code);
    }

    public async Task AddAsync(Subject subject)
 {
        await _context.Subjects.AddAsync(subject);
    }

public void Update(Subject subject)
    {
        _context.Subjects.Update(subject);
    }

    public void Delete(Subject subject)
    {
    _context.Subjects.Remove(subject);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
