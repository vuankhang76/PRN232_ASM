using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers.OData;

/// <summary>
/// OData controller for exams dashboard
/// </summary>
[Authorize(Roles = "Admin,Manager,Moderator")]
public class ExamsODataController : ODataController
{
    private readonly Prn232GradingContext _context;

    public ExamsODataController(Prn232GradingContext context)
    {
        _context = context;
    }

    [EnableQuery(PageSize = 50, MaxExpansionDepth = 3)]
public IActionResult Get()
    {
    var exams = _context.Exams
  .Include(e => e.Subject)
         .Include(e => e.Semester)
       .Include(e => e.RubricItems)
  .Include(e => e.Submissions)
      .ThenInclude(s => s.Student)
  .AsQueryable();

        return Ok(exams);
    }
}
