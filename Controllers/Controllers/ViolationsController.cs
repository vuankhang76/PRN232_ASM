using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;
using Controllers.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Controllers.Controllers;

/// <summary>
/// Violation management controller
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ViolationsController : ControllerBase
{
    private readonly IViolationService _violationService;
    private readonly IHubContext<GradingHub> _hubContext;

    public ViolationsController(IViolationService violationService, IHubContext<GradingHub> hubContext)
    {
     _violationService = violationService;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Get all violations
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Violation>>> GetAllViolations()
    {
        var violations = await _violationService.GetAllViolationsAsync();
        return Ok(violations);
    }

    /// <summary>
    /// Get violation by ID
    /// </summary>
    [HttpGet("{id}")]
  public async Task<ActionResult<Violation>> GetViolationById(long id)
    {
      var violation = await _violationService.GetViolationByIdAsync(id);
        
        if (violation == null)
  return NotFound($"Violation with ID '{id}' not found.");

        return Ok(violation);
    }

    /// <summary>
    /// Get violations by submission
    /// </summary>
    [HttpGet("submission/{submissionId}")]
    public async Task<ActionResult<IEnumerable<Violation>>> GetViolationsBySubmission(long submissionId)
    {
   var violations = await _violationService.GetViolationsBySubmissionIdAsync(submissionId);
   return Ok(violations);
    }

    /// <summary>
    /// Get violations by exam
    /// </summary>
    [HttpGet("exam/{examId}")]
    public async Task<ActionResult<IEnumerable<Violation>>> GetViolationsByExam(int examId)
    {
  var violations = await _violationService.GetViolationsByExamIdAsync(examId);
     return Ok(violations);
    }

    /// <summary>
    /// Create violation (Manager/Moderator only)
    /// </summary>
[Authorize(Roles = "Manager,Moderator,Admin")]
    [HttpPost]
    public async Task<ActionResult<Violation>> CreateViolation([FromBody] CreateViolationRequest request)
    {
   try
        {
    var violation = await _violationService.CreateViolationAsync(
   request.SubmissionId,
   request.ViolationTypeId,
  request.Message,
                request.Severity,
     request.Evidence);

       // Notify via SignalR
   await _hubContext.Clients.All.SendAsync("ViolationDetected", 
  violation.SubmissionId,
         violation.Submission.Student.StudentCode,
    violation.ViolationType.Name);

  return CreatedAtAction(nameof(GetViolationById), new { id = violation.ViolationId }, violation);
        }
        catch (InvalidOperationException ex)
        {
  return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete violation (Manager/Admin only)
    /// </summary>
    [Authorize(Roles = "Manager,Admin")]
[HttpDelete("{id}")]
    public async Task<ActionResult> DeleteViolation(long id)
    {
  var result = await _violationService.DeleteViolationAsync(id);
        
        if (!result)
     return NotFound($"Violation with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class CreateViolationRequest
{
    public long SubmissionId { get; set; }
    public int ViolationTypeId { get; set; }
    public string Message { get; set; } = null!;
    public byte Severity { get; set; } = 1;
    public string? Evidence { get; set; }
}
