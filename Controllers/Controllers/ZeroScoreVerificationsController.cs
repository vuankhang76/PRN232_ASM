using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Zero score verification controller for moderators
/// </summary>
[Authorize(Roles = "Moderator,Admin")]
[ApiController]
[Route("api/[controller]")]
public class ZeroScoreVerificationsController : ControllerBase
{
    private readonly IZeroScoreVerificationService _verificationService;

    public ZeroScoreVerificationsController(IZeroScoreVerificationService verificationService)
    {
   _verificationService = verificationService;
    }

    /// <summary>
    /// Get all zero score verifications
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ZeroScoreVerification>>> GetAllVerifications()
    {
  var verifications = await _verificationService.GetAllVerificationsAsync();
   return Ok(verifications);
    }

    /// <summary>
 /// Get verification by ID
 /// </summary>
[HttpGet("{id}")]
    public async Task<ActionResult<ZeroScoreVerification>> GetVerificationById(long id)
    {
  var verification = await _verificationService.GetVerificationByIdAsync(id);
        
   if (verification == null)
      return NotFound($"Verification with ID '{id}' not found.");

   return Ok(verification);
    }

    /// <summary>
 /// Get verification by submission
    /// </summary>
    [HttpGet("submission/{submissionId}")]
  public async Task<ActionResult<ZeroScoreVerification>> GetVerificationBySubmission(long submissionId)
    {
   var verification = await _verificationService.GetVerificationBySubmissionAsync(submissionId);
   
        if (verification == null)
   return NotFound($"No verification found for submission '{submissionId}'.");

    return Ok(verification);
    }

 /// <summary>
    /// Get verifications by moderator
    /// </summary>
    [HttpGet("moderator/{moderatorId}")]
    public async Task<ActionResult<IEnumerable<ZeroScoreVerification>>> GetVerificationsByModerator(Guid moderatorId)
 {
        var verifications = await _verificationService.GetVerificationsByModeratorAsync(moderatorId);
        return Ok(verifications);
    }

    /// <summary>
    /// Create zero score verification
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ZeroScoreVerification>> CreateVerification([FromBody] CreateVerificationRequest request)
    {
   try
        {
    var verification = await _verificationService.CreateVerificationAsync(
   request.SubmissionId,
 request.ModeratorId,
     request.Reason);

      return CreatedAtAction(nameof(GetVerificationById), new { id = verification.VerificationId }, verification);
 }
    catch (InvalidOperationException ex)
        {
  return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete verification (Admin only)
 /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteVerification(long id)
    {
   var result = await _verificationService.DeleteVerificationAsync(id);
  
   if (!result)
            return NotFound($"Verification with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class CreateVerificationRequest
{
    public long SubmissionId { get; set; }
    public Guid ModeratorId { get; set; }
 public string? Reason { get; set; }
}
