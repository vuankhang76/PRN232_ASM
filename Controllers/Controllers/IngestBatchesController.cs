using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using Services;
using Controllers.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Controllers.Controllers;

/// <summary>
/// Ingest batch controller for file upload management
/// </summary>
[Authorize(Roles = "Admin,Manager")]
[ApiController]
[Route("api/[controller]")]
public class IngestBatchesController : ControllerBase
{
    private readonly IIngestBatchService _batchService;
    private readonly IHubContext<GradingHub> _hubContext;

 public IngestBatchesController(IIngestBatchService batchService, IHubContext<GradingHub> hubContext)
    {
        _batchService = batchService;
        _hubContext = hubContext;
    }

 /// <summary>
    /// Get all batches
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IngestBatch>>> GetAllBatches()
    {
        var batches = await _batchService.GetAllBatchesAsync();
        return Ok(batches);
    }

    /// <summary>
    /// Get batch by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<IngestBatch>> GetBatchById(long id)
    {
 var batch = await _batchService.GetBatchByIdAsync(id);
     
        if (batch == null)
            return NotFound($"Batch with ID '{id}' not found.");

        return Ok(batch);
    }

  /// <summary>
    /// Get batch with files
/// </summary>
  [HttpGet("{id}/files")]
    public async Task<ActionResult<IngestBatch>> GetBatchWithFiles(long id)
    {
        var batch = await _batchService.GetBatchWithFilesAsync(id);
        
 if (batch == null)
   return NotFound($"Batch with ID '{id}' not found.");

   return Ok(batch);
    }

    /// <summary>
/// Get batches by exam
    /// </summary>
    [HttpGet("exam/{examId}")]
    public async Task<ActionResult<IEnumerable<IngestBatch>>> GetBatchesByExam(int examId)
{
        var batches = await _batchService.GetBatchesByExamAsync(examId);
   return Ok(batches);
    }

    /// <summary>
    /// Create new batch
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<IngestBatch>> CreateBatch([FromBody] CreateBatchRequest request)
 {
        try
        {
   var batch = await _batchService.CreateBatchAsync(
    request.ExamId,
request.SourceArchivePath,
                request.ExtractRootPath);

       // Notify via SignalR
 await _hubContext.Clients.All.SendAsync("BatchCreated", batch.BatchId, batch.ExamId);

            return CreatedAtAction(nameof(GetBatchById), new { id = batch.BatchId }, batch);
     }
        catch (InvalidOperationException ex)
        {
   return BadRequest(ex.Message);
     }
    }

    /// <summary>
    /// Update batch status
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<IngestBatch>> UpdateBatchStatus(long id, [FromBody] UpdateBatchStatusRequest request)
    {
        try
  {
   var batch = await _batchService.UpdateBatchStatusAsync(id, request.Status, request.Message);
            
      // Notify via SignalR
   await _hubContext.Clients.All.SendAsync("BatchStatusUpdated", id, request.Status);

      return Ok(batch);
      }
  catch (InvalidOperationException ex)
   {
   return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete batch (Admin only)
    /// </summary>
  [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBatch(long id)
    {
        var result = await _batchService.DeleteBatchAsync(id);
      
        if (!result)
   return NotFound($"Batch with ID '{id}' not found.");

  return NoContent();
    }
}

// Request models
public class CreateBatchRequest
{
    public int ExamId { get; set; }
    public string SourceArchivePath { get; set; } = null!;
    public string? ExtractRootPath { get; set; }
}

public class UpdateBatchStatusRequest
{
    public string Status { get; set; } = null!;
    public string? Message { get; set; }
}
