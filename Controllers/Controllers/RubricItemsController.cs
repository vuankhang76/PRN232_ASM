using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Rubric item management controller
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RubricItemsController : ControllerBase
{
    private readonly IRubricItemService _rubricItemService;

    public RubricItemsController(IRubricItemService rubricItemService)
    {
   _rubricItemService = rubricItemService;
    }

    /// <summary>
    /// Get all rubric items
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RubricItem>>> GetAllRubricItems()
    {
        var rubricItems = await _rubricItemService.GetAllRubricItemsAsync();
   return Ok(rubricItems);
    }

    /// <summary>
    /// Get rubric item by ID
    /// </summary>
  [HttpGet("{id}")]
  public async Task<ActionResult<RubricItem>> GetRubricItemById(long id)
    {
        var rubricItem = await _rubricItemService.GetRubricItemByIdAsync(id);
  
   if (rubricItem == null)
  return NotFound($"RubricItem with ID '{id}' not found.");

        return Ok(rubricItem);
    }

    /// <summary>
    /// Get rubric items by exam
  /// </summary>
 [HttpGet("exam/{examId}")]
    public async Task<ActionResult<IEnumerable<RubricItem>>> GetRubricItemsByExam(int examId)
    {
   var rubricItems = await _rubricItemService.GetRubricItemsByExamIdAsync(examId);
  return Ok(rubricItems);
    }

    /// <summary>
    /// Create rubric item (Admin/Manager only)
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<ActionResult<RubricItem>> CreateRubricItem([FromBody] CreateRubricItemRequest request)
    {
        try
      {
        var rubricItem = await _rubricItemService.CreateRubricItemAsync(
     request.ExamId,
   request.Code,
    request.Title,
       request.MaxPoint,
    request.Keywords,
    request.OrderNo);

return CreatedAtAction(nameof(GetRubricItemById), new { id = rubricItem.RubricItemId }, rubricItem);
 }
        catch (InvalidOperationException ex)
     {
      return BadRequest(ex.Message);
  }
    }

 /// <summary>
/// Update rubric item (Admin/Manager only)
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<ActionResult<RubricItem>> UpdateRubricItem(long id, [FromBody] UpdateRubricItemRequest request)
    {
    try
  {
        var rubricItem = await _rubricItemService.UpdateRubricItemAsync(
   id,
request.Title,
           request.MaxPoint,
   request.Keywords,
 request.OrderNo);

   return Ok(rubricItem);
  }
        catch (InvalidOperationException ex)
   {
 return NotFound(ex.Message);
}
    }

    /// <summary>
    /// Delete rubric item (Admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRubricItem(long id)
    {
        var result = await _rubricItemService.DeleteRubricItemAsync(id);
    
if (!result)
        return NotFound($"RubricItem with ID '{id}' not found.");

        return NoContent();
    }
}

// Request models
public class CreateRubricItemRequest
{
    public int ExamId { get; set; }
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal MaxPoint { get; set; }
    public string? Keywords { get; set; }
    public int? OrderNo { get; set; }
}

public class UpdateRubricItemRequest
{
    public string Title { get; set; } = null!;
    public decimal MaxPoint { get; set; }
    public string? Keywords { get; set; }
    public int? OrderNo { get; set; }
}
