using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;
using Controllers.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Controllers.Controllers;

/// <summary>
/// Examiner assignment controller for managers
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IExaminerAssignmentService _assignmentService;
    private readonly IHubContext<GradingHub> _hubContext;

    public AssignmentsController(IExaminerAssignmentService assignmentService, IHubContext<GradingHub> hubContext)
    {
  _assignmentService = assignmentService;
   _hubContext = hubContext;
    }

    /// <summary>
/// Get all assignments
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExaminerAssignment>>> GetAllAssignments()
    {
        var assignments = await _assignmentService.GetAllAssignmentsAsync();
 return Ok(assignments);
    }

    /// <summary>
    /// Get assignment by ID
    /// </summary>
 [HttpGet("{id}")]
    public async Task<ActionResult<ExaminerAssignment>> GetAssignmentById(long id)
    {
var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
        
        if (assignment == null)
     return NotFound($"Assignment with ID '{id}' not found.");

return Ok(assignment);
}

    /// <summary>
    /// Get assignment with scores
    /// </summary>
    [HttpGet("{id}/scores")]
    public async Task<ActionResult<ExaminerAssignment>> GetAssignmentWithScores(long id)
{
        var assignment = await _assignmentService.GetAssignmentWithScoresAsync(id);
   
        if (assignment == null)
  return NotFound($"Assignment with ID '{id}' not found.");

        return Ok(assignment);
    }

    /// <summary>
    /// Get assignments by submission
    /// </summary>
[HttpGet("submission/{submissionId}")]
public async Task<ActionResult<IEnumerable<ExaminerAssignment>>> GetAssignmentsBySubmission(long submissionId)
    {
        var assignments = await _assignmentService.GetAssignmentsBySubmissionIdAsync(submissionId);
  return Ok(assignments);
    }

/// <summary>
 /// Get assignments for examiner
    /// </summary>
    [HttpGet("examiner/{examinerId}")]
public async Task<ActionResult<IEnumerable<ExaminerAssignment>>> GetAssignmentsByExaminer(Guid examinerId)
    {
   var assignments = await _assignmentService.GetAssignmentsByExaminerIdAsync(examinerId);
  return Ok(assignments);
    }

    /// <summary>
    /// Get assignments by exam
/// </summary>
    [HttpGet("exam/{examId}")]
  public async Task<ActionResult<IEnumerable<ExaminerAssignment>>> GetAssignmentsByExam(int examId)
    {
      var assignments = await _assignmentService.GetAssignmentsByExamIdAsync(examId);
  return Ok(assignments);
    }

    /// <summary>
    /// Assign examiner to submission (Manager only)
    /// </summary>
    [Authorize(Roles = "Manager,Admin")]
    [HttpPost]
    public async Task<ActionResult<ExaminerAssignment>> AssignExaminer([FromBody] AssignExaminerRequest request)
    {
      try
  {
       var assignment = await _assignmentService.AssignExaminerAsync(
 request.SubmissionId,
    request.ExaminerId,
     request.AssignedBy);

          // Notify via SignalR
       await _hubContext.Clients.All.SendAsync("ExaminerAssigned", 
   assignment.AssignmentId, 
      assignment.Examiner.FullName, 
       assignment.SubmissionId);

      return CreatedAtAction(nameof(GetAssignmentById), new { id = assignment.AssignmentId }, assignment);
        }
      catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update assignment status
    /// </summary>
  [HttpPut("{id}/status")]
    public async Task<ActionResult<ExaminerAssignment>> UpdateAssignmentStatus(long id, [FromBody] UpdateStatusRequest request)
    {
      try
        {
   var assignment = await _assignmentService.UpdateAssignmentStatusAsync(id, request.Status);
   return Ok(assignment);
        }
        catch (InvalidOperationException ex)
        {
     return NotFound(ex.Message);
      }
    }

  /// <summary>
  /// Delete assignment (Manager only)
    /// </summary>
    [Authorize(Roles = "Manager,Admin")]
[HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAssignment(long id)
    {
      var result = await _assignmentService.DeleteAssignmentAsync(id);
        
        if (!result)
     return NotFound($"Assignment with ID '{id}' not found.");

return NoContent();
    }
}

// Request models
public class AssignExaminerRequest
{
    public long SubmissionId { get; set; }
    public Guid ExaminerId { get; set; }
public Guid? AssignedBy { get; set; }
}

public class UpdateStatusRequest
{
    public string Status { get; set; } = null!;
}
