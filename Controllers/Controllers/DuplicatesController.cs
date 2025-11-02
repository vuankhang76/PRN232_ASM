using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Duplicate detection controller
/// </summary>
[Authorize(Roles = "Admin,Manager,Moderator")]
[ApiController]
[Route("api/[controller]")]
public class DuplicatesController : ControllerBase
{
    private readonly IDuplicateGroupService _duplicateGroupService;

    public DuplicatesController(IDuplicateGroupService duplicateGroupService)
    {
        _duplicateGroupService = duplicateGroupService;
    }

    /// <summary>
    /// Get all duplicate groups
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DuplicateGroup>>> GetAllDuplicateGroups()
    {
 var groups = await _duplicateGroupService.GetAllDuplicateGroupsAsync();
  return Ok(groups);
    }

    /// <summary>
    /// Get duplicate group by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DuplicateGroup>> GetDuplicateGroupById(long id)
 {
        var group = await _duplicateGroupService.GetDuplicateGroupByIdAsync(id);
        
        if (group == null)
     return NotFound($"Duplicate group with ID '{id}' not found.");

      return Ok(group);
    }

    /// <summary>
    /// Get duplicate group with members
    /// </summary>
[HttpGet("{id}/members")]
public async Task<ActionResult<DuplicateGroup>> GetGroupWithMembers(long id)
    {
    var group = await _duplicateGroupService.GetGroupWithMembersAsync(id);
        
        if (group == null)
   return NotFound($"Duplicate group with ID '{id}' not found.");

        return Ok(group);
    }

    /// <summary>
    /// Get duplicate groups by exam
    /// </summary>
    [HttpGet("exam/{examId}")]
    public async Task<ActionResult<IEnumerable<DuplicateGroup>>> GetDuplicateGroupsByExam(int examId)
    {
        var groups = await _duplicateGroupService.GetDuplicateGroupsByExamAsync(examId);
        return Ok(groups);
    }

 /// <summary>
    /// Get high similarity groups (>= threshold)
    /// </summary>
    [HttpGet("exam/{examId}/high-similarity")]
    public async Task<ActionResult<IEnumerable<DuplicateGroup>>> GetHighSimilarityGroups(
        int examId, 
        [FromQuery] decimal minSimilarity = 80.0m)
    {
      var groups = await _duplicateGroupService.GetHighSimilarityGroupsAsync(examId, minSimilarity);
        return Ok(groups);
    }

    /// <summary>
    /// Create duplicate group (Admin/Manager only)
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<ActionResult<DuplicateGroup>> CreateDuplicateGroup([FromBody] CreateDuplicateGroupRequest request)
    {
try
        {
  var group = await _duplicateGroupService.CreateDuplicateGroupAsync(
   request.ExamId,
       request.Similarity,
    request.ReportPath);

       return CreatedAtAction(nameof(GetDuplicateGroupById), new { id = group.GroupId }, group);
        }
        catch (InvalidOperationException ex)
   {
        return BadRequest(ex.Message);
 }
    }

 /// <summary>
    /// Delete duplicate group (Admin only)
/// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDuplicateGroup(long id)
    {
 var result = await _duplicateGroupService.DeleteDuplicateGroupAsync(id);
        
  if (!result)
  return NotFound($"Duplicate group with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class CreateDuplicateGroupRequest
{
  public int ExamId { get; set; }
    public decimal Similarity { get; set; }
    public string? ReportPath { get; set; }
}
