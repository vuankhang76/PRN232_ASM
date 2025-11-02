using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers.OData;

/// <summary>
/// OData controller for violations dashboard
/// </summary>
[Authorize(Roles = "Admin,Manager,Moderator")]
public class ViolationsODataController : ODataController
{
    private readonly Prn232GradingContext _context;

    public ViolationsODataController(Prn232GradingContext context)
    {
   _context = context;
    }

    [EnableQuery(PageSize = 50, MaxExpansionDepth = 3)]
    public IActionResult Get()
    {
  var violations = _context.Violations
       .Include(v => v.Submission)
   .ThenInclude(s => s.Student)
   .Include(v => v.Submission)
     .ThenInclude(s => s.Exam)
            .Include(v => v.ViolationType)
      .AsQueryable();

     return Ok(violations);
    }
}
