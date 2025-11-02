using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;
using Controllers.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Controllers.Controllers;

/// <summary>
/// Examiner scoring controller
/// </summary>
[Authorize(Roles = "Examiner,Admin,Manager")]
[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IExaminerScoreService _scoreService;
    private readonly IHubContext<GradingHub> _hubContext;

 public ScoresController(IExaminerScoreService scoreService, IHubContext<GradingHub> hubContext)
    {
        _scoreService = scoreService;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Get all scores
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExaminerScore>>> GetAllScores()
    {
   var scores = await _scoreService.GetAllScoresAsync();
   return Ok(scores);
    }

    /// <summary>
    /// Get score by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ExaminerScore>> GetScoreById(long id)
    {
   var score = await _scoreService.GetScoreByIdAsync(id);
   
      if (score == null)
  return NotFound($"Score with ID '{id}' not found.");

        return Ok(score);
    }

    /// <summary>
  /// Get scores by assignment
    /// </summary>
    [HttpGet("assignment/{assignmentId}")]
    public async Task<ActionResult<IEnumerable<ExaminerScore>>> GetScoresByAssignment(long assignmentId)
    {
        var scores = await _scoreService.GetScoresByAssignmentIdAsync(assignmentId);
        return Ok(scores);
    }

  /// <summary>
    /// Get total score for assignment
    /// </summary>
    [HttpGet("assignment/{assignmentId}/total")]
 public async Task<ActionResult<decimal>> GetTotalScore(long assignmentId)
  {
     var totalScore = await _scoreService.CalculateTotalScoreForAssignmentAsync(assignmentId);
    return Ok(new { assignmentId, totalScore });
    }

    /// <summary>
    /// Submit score for rubric item
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ExaminerScore>> SubmitScore([FromBody] SubmitScoreRequest request)
    {
        try
      {
   var score = await _scoreService.SubmitScoreAsync(
   request.AssignmentId,
request.RubricItemId,
   request.Score,
      request.Comment);

      // Notify via SignalR
            var totalScore = await _scoreService.CalculateTotalScoreForAssignmentAsync(request.AssignmentId);
await _hubContext.Clients.All.SendAsync("ScoreSubmitted", 
   request.AssignmentId, 
   request.RubricItemId,
request.Score,
   totalScore);

      return CreatedAtAction(nameof(GetScoreById), new { id = score.ExaminerScoreId }, score);
        }
      catch (InvalidOperationException ex)
        {
  return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update score
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ExaminerScore>> UpdateScore(long id, [FromBody] UpdateScoreRequest request)
    {
        try
  {
            var score = await _scoreService.UpdateScoreAsync(id, request.Score, request.Comment);
  
      // Notify via SignalR
            await _hubContext.Clients.All.SendAsync("ScoreUpdated", id, request.Score);

return Ok(score);
}
     catch (InvalidOperationException ex)
     {
    return NotFound(ex.Message);
    }
 }

 /// <summary>
    /// Delete score
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteScore(long id)
    {
        var result = await _scoreService.DeleteScoreAsync(id);
        
     if (!result)
       return NotFound($"Score with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class SubmitScoreRequest
{
  public long AssignmentId { get; set; }
    public int RubricItemId { get; set; }
    public decimal Score { get; set; }
    public string? Comment { get; set; }
}

public class UpdateScoreRequest
{
 public decimal Score { get; set; }
    public string? Comment { get; set; }
}
