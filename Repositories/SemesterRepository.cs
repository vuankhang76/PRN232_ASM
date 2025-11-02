using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Semester repository implementation for managing Semester entities
/// </summary>
public class SemesterRepository : ISemesterRepository
{
    private readonly Prn232GradingContext _context;

    public SemesterRepository(Prn232GradingContext context)
    {
        _context = context;
}

    public async Task<IEnumerable<Semester>> GetAllAsync()
    {
        return await _context.Semesters
 .OrderByDescending(s => s.Code)
       .ToListAsync();
    }

    public async Task<Semester?> GetByIdAsync(int semesterId)
 {
 return await _context.Semesters
      .FirstOrDefaultAsync(s => s.SemesterId == semesterId);
    }

    public async Task<Semester?> GetByCodeAsync(string code)
    {
 return await _context.Semesters
   .FirstOrDefaultAsync(s => s.Code == code);
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
   return await _context.Semesters
   .AnyAsync(s => s.Code == code);
    }

public async Task AddAsync(Semester semester)
    {
   await _context.Semesters.AddAsync(semester);
    }

    public void Update(Semester semester)
    {
        _context.Semesters.Update(semester);
  }

 public void Delete(Semester semester)
    {
        _context.Semesters.Remove(semester);
    }

    public async Task<bool> SaveChangesAsync()
    {
  return await _context.SaveChangesAsync() > 0;
    }
}
