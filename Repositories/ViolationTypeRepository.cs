using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// ViolationType repository implementation for managing violation types
/// </summary>
public class ViolationTypeRepository : IViolationTypeRepository
{
    private readonly Prn232GradingContext _context;

  public ViolationTypeRepository(Prn232GradingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ViolationType>> GetAllAsync()
    {
        return await _context.ViolationTypes
 .OrderBy(vt => vt.Code)
          .ToListAsync();
    }

    public async Task<ViolationType?> GetByIdAsync(int violationTypeId)
  {
   return await _context.ViolationTypes
       .FirstOrDefaultAsync(vt => vt.ViolationTypeId == violationTypeId);
}

    public async Task<ViolationType?> GetByCodeAsync(string code)
    {
     return await _context.ViolationTypes
       .FirstOrDefaultAsync(vt => vt.Code == code);
    }

    public async Task<bool> CodeExistsAsync(string code)
 {
   return await _context.ViolationTypes
            .AnyAsync(vt => vt.Code == code);
 }

    public async Task AddAsync(ViolationType violationType)
    {
 await _context.ViolationTypes.AddAsync(violationType);
    }

    public void Update(ViolationType violationType)
 {
        _context.ViolationTypes.Update(violationType);
    }

public void Delete(ViolationType violationType)
    {
        _context.ViolationTypes.Remove(violationType);
    }

    public async Task<bool> SaveChangesAsync()
    {
 return await _context.SaveChangesAsync() > 0;
    }
}
