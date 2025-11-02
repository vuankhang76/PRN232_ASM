using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Subject management controller
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
 {
      _subjectService = subjectService;
    }

    /// <summary>
    /// Get all subjects
    /// </summary>
 [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetAllSubjects()
  {
     var subjects = await _subjectService.GetAllSubjectsAsync();
   return Ok(subjects);
    }

 /// <summary>
    /// Get subject by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubjectById(int id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
   
   if (subject == null)
       return NotFound($"Subject with ID '{id}' not found.");

        return Ok(subject);
}

 /// <summary>
    /// Get subject by code
    /// </summary>
    [HttpGet("code/{code}")]
    public async Task<ActionResult<Subject>> GetSubjectByCode(string code)
    {
        var subject = await _subjectService.GetSubjectByCodeAsync(code);
        
if (subject == null)
   return NotFound($"Subject with code '{code}' not found.");

 return Ok(subject);
    }

 /// <summary>
    /// Create subject (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Subject>> CreateSubject([FromBody] CreateSubjectRequest request)
    {
     try
   {
  var subject = await _subjectService.CreateSubjectAsync(request.Code, request.Name);
      return CreatedAtAction(nameof(GetSubjectById), new { id = subject.SubjectId }, subject);
     }
  catch (InvalidOperationException ex)
     {
            return BadRequest(ex.Message);
        }
    }

 /// <summary>
/// Update subject (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Subject>> UpdateSubject(int id, [FromBody] UpdateSubjectRequest request)
    {
   try
   {
   var subject = await _subjectService.UpdateSubjectAsync(id, request.Name);
     return Ok(subject);
  }
  catch (InvalidOperationException ex)
      {
  return NotFound(ex.Message);
     }
    }

    /// <summary>
    /// Delete subject (Admin only)
    /// </summary>
[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubject(int id)
    {
  var result = await _subjectService.DeleteSubjectAsync(id);
   
        if (!result)
   return NotFound($"Subject with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class CreateSubjectRequest
{
    public string Code { get; set; } = null!;
  public string Name { get; set; } = null!;
}

public class UpdateSubjectRequest
{
    public string Name { get; set; } = null!;
}
