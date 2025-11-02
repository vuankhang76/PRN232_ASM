using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Semester management controller
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SemestersController : ControllerBase
{
    private readonly ISemesterService _semesterService;

    public SemestersController(ISemesterService semesterService)
    {
        _semesterService = semesterService;
  }

    /// <summary>
    /// Get all semesters
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Semester>>> GetAllSemesters()
    {
   var semesters = await _semesterService.GetAllSemestersAsync();
        return Ok(semesters);
}

    /// <summary>
    /// Get semester by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Semester>> GetSemesterById(int id)
    {
        var semester = await _semesterService.GetSemesterByIdAsync(id);
      
      if (semester == null)
     return NotFound($"Semester with ID '{id}' not found.");

  return Ok(semester);
    }

    /// <summary>
    /// Get semester by code
    /// </summary>
    [HttpGet("code/{code}")]
  public async Task<ActionResult<Semester>> GetSemesterByCode(string code)
 {
     var semester = await _semesterService.GetSemesterByCodeAsync(code);
     
   if (semester == null)
      return NotFound($"Semester with code '{code}' not found.");

   return Ok(semester);
    }

    /// <summary>
    /// Create semester (Admin only)
    /// </summary>
  [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Semester>> CreateSemester([FromBody] CreateSemesterRequest request)
    {
   try
   {
     var semester = await _semesterService.CreateSemesterAsync(request.Code, request.Name);
return CreatedAtAction(nameof(GetSemesterById), new { id = semester.SemesterId }, semester);
    }
     catch (InvalidOperationException ex)
   {
  return BadRequest(ex.Message);
        }
    }

    /// <summary>
  /// Update semester (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
  [HttpPut("{id}")]
    public async Task<ActionResult<Semester>> UpdateSemester(int id, [FromBody] UpdateSemesterRequest request)
    {
        try
     {
  var semester = await _semesterService.UpdateSemesterAsync(id, request.Name);
   return Ok(semester);
        }
  catch (InvalidOperationException ex)
   {
  return NotFound(ex.Message);
        }
 }

    /// <summary>
    /// Delete semester (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
  [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSemester(int id)
    {
   var result = await _semesterService.DeleteSemesterAsync(id);
        
   if (!result)
 return NotFound($"Semester with ID '{id}' not found.");

  return NoContent();
    }
}

// Request models
public class CreateSemesterRequest
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class UpdateSemesterRequest
{
    public string Name { get; set; } = null!;
}
