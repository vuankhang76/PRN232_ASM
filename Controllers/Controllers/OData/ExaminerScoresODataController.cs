using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers.OData;

/// <summary>
/// OData controller for examiner scores dashboard
/// </summary>
[Authorize(Roles = "Admin,Manager,Moderator")]
public class ExaminerScoresODataController : ODataController
{
 private readonly Prn232GradingContext _context;

    public ExaminerScoresODataController(Prn232GradingContext context)
  {
        _context = context;
  }

    [EnableQuery(PageSize = 50, MaxExpansionDepth = 3)]
    public IActionResult Get()
  {
   var scores = _context.ExaminerScores
      .Include(es => es.Assignment)
 .ThenInclude(a => a.Examiner)
    .Include(es => es.Assignment)
    .ThenInclude(a => a.Submission)
     .ThenInclude(s => s.Student)
            .Include(es => es.RubricItem)
  .AsQueryable();

     return Ok(scores);
    }
}
