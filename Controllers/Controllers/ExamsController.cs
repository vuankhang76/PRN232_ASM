using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Exam management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
    private readonly IExamService _examService;

    public ExamsController(IExamService examService)
    {
     _examService = examService;
    }

    /// <summary>
    /// Get all exams
    /// </summary>
  [HttpGet]
public async Task<ActionResult<IEnumerable<Exam>>> GetAllExams()
  {
        var exams = await _examService.GetAllExamsAsync();
   return Ok(exams);
    }

    /// <summary>
    /// Get exam by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Exam>> GetExamById(int id)
    {
   var exam = await _examService.GetExamByIdAsync(id);
        
        if (exam == null)
   return NotFound($"Exam with ID '{id}' not found.");

        return Ok(exam);
    }

    /// <summary>
  /// Get exam with full details by ID
    /// </summary>
  [HttpGet("{id}/details")]
    public async Task<ActionResult<Exam>> GetExamWithDetails(int id)
    {
        var exam = await _examService.GetExamWithDetailsAsync(id);
     
        if (exam == null)
     return NotFound($"Exam with ID '{id}' not found.");

  return Ok(exam);
    }

    /// <summary>
    /// Get exams by semester
    /// </summary>
    [HttpGet("semester/{semesterId}")]
public async Task<ActionResult<IEnumerable<Exam>>> GetExamsBySemester(int semesterId)
    {
        var exams = await _examService.GetExamsBySemesterAsync(semesterId);
   return Ok(exams);
    }

    /// <summary>
    /// Get exams by subject
 /// </summary>
 [HttpGet("subject/{subjectId}")]
    public async Task<ActionResult<IEnumerable<Exam>>> GetExamsBySubject(int subjectId)
    {
        var exams = await _examService.GetExamsBySubjectAsync(subjectId);
        return Ok(exams);
    }

    /// <summary>
    /// Get exam by exam code
    /// </summary>
    [HttpGet("code/{examCode}")]
    public async Task<ActionResult<Exam>> GetByExamCode(string examCode)
    {
   var exam = await _examService.GetByExamCodeAsync(examCode);
      
  if (exam == null)
 return NotFound($"Exam with code '{examCode}' not found.");

        return Ok(exam);
    }

    /// <summary>
/// Create new exam
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Exam>> CreateExam([FromBody] CreateExamRequest request)
    {
try
    {
       var exam = await _examService.CreateExamAsync(
    request.SubjectId,
        request.SemesterId,
    request.ExamCode,
     request.ExamName,
      request.Description,
        request.ExamPaperPath,
    request.MarkingSheetPath);
            
            return CreatedAtAction(nameof(GetExamById), new { id = exam.ExamId }, exam);
        }
        catch (Exception ex)
        {
       return BadRequest(ex.Message);
    }
    }

    /// <summary>
    /// Update exam
 /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Exam>> UpdateExam(int id, [FromBody] UpdateExamRequest request)
  {
        try
        {
 var exam = await _examService.UpdateExamAsync(
                id,
     request.ExamName,
       request.Description,
     request.ExamPaperPath,
                request.MarkingSheetPath);
            
      return Ok(exam);
        }
        catch (InvalidOperationException ex)
{
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete exam
    /// </summary>
    [HttpDelete("{id}")]
 public async Task<ActionResult> DeleteExam(int id)
    {
        var result = await _examService.DeleteExamAsync(id);
        
   if (!result)
     return NotFound($"Exam with ID '{id}' not found.");

  return NoContent();
    }
}

// Request models
public class CreateExamRequest
{
    public int SubjectId { get; set; }
    public int SemesterId { get; set; }
  public string ExamCode { get; set; } = null!;
    public string ExamName { get; set; } = null!;
    public string? Description { get; set; }
    public string? ExamPaperPath { get; set; }
    public string? MarkingSheetPath { get; set; }
}

public class UpdateExamRequest
{
    public string ExamName { get; set; } = null!;
    public string? Description { get; set; }
    public string? ExamPaperPath { get; set; }
    public string? MarkingSheetPath { get; set; }
}
