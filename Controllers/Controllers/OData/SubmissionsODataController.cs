using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers.OData;

/// <summary>
/// OData controller for submissions dashboard
/// </summary>
[Authorize(Roles = "Admin,Manager,Moderator")]
public class SubmissionsODataController : ODataController
{
    private readonly Prn232GradingContext _context;

    public SubmissionsODataController(Prn232GradingContext context)
    {
        _context = context;
    }

    [EnableQuery(PageSize = 50, MaxExpansionDepth = 3)]
    public IActionResult Get()
    {
  var submissions = _context.Submissions
      .Include(s => s.Student)
       .Include(s => s.Exam)
    .ThenInclude(e => e.Subject)
      .Include(s => s.Exam)
         .ThenInclude(e => e.Semester)
            .Include(s => s.ExaminerAssignments)
      .Include(s => s.Violations)
         .Include(s => s.FinalGrade)
       .AsQueryable();

     return Ok(submissions);
    }
}
