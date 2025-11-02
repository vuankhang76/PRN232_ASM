using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Student management controller
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
    _studentService = studentService;
    }

  /// <summary>
 /// Get all students
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
   return Ok(students);
    }

    /// <summary>
    /// Get student by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudentById(int id)
    {
  var student = await _studentService.GetStudentByIdAsync(id);
   
        if (student == null)
     return NotFound($"Student with ID '{id}' not found.");

 return Ok(student);
    }

    /// <summary>
    /// Get student by code
    /// </summary>
    [HttpGet("code/{studentCode}")]
    public async Task<ActionResult<Student>> GetStudentByCode(string studentCode)
{
        var student = await _studentService.GetStudentByCodeAsync(studentCode);
        
        if (student == null)
   return NotFound($"Student with code '{studentCode}' not found.");

 return Ok(student);
    }

    /// <summary>
/// Get student with submissions
    /// </summary>
[HttpGet("{id}/submissions")]
    public async Task<ActionResult<Student>> GetStudentWithSubmissions(int id)
    {
        var student = await _studentService.GetStudentWithSubmissionsAsync(id);
   
     if (student == null)
  return NotFound($"Student with ID '{id}' not found.");

        return Ok(student);
    }

    /// <summary>
    /// Create new student (Admin/Manager only)
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentRequest request)
    {
        try
      {
     var student = await _studentService.CreateStudentAsync(
  request.StudentCode,
    request.FullName,
     request.Email,
    request.ClassName);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }
        catch (InvalidOperationException ex)
        {
   return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update student (Admin/Manager only)
 /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] UpdateStudentRequest request)
    {
        try
        {
 var student = await _studentService.UpdateStudentAsync(id, request.FullName, request.Email, request.ClassName);
   return Ok(student);
  }
  catch (InvalidOperationException ex)
        {
     return NotFound(ex.Message);
        }
 }

  /// <summary>
    /// Delete student (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(int id)
 {
   var result = await _studentService.DeleteStudentAsync(id);
        
   if (!result)
   return NotFound($"Student with ID '{id}' not found.");

        return NoContent();
  }
}

// Request models
public class CreateStudentRequest
{
public string StudentCode { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Email { get; set; }
  public string? ClassName { get; set; }
}

public class UpdateStudentRequest
{
    public string FullName { get; set; } = null!;
  public string? Email { get; set; }
    public string? ClassName { get; set; }
}
