using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers.Controllers;

/// <summary>
/// Excel export controller
/// </summary>
[Authorize(Roles = "Admin,Manager")]
[ApiController]
[Route("api/[controller]")]
public class ExportsController : ControllerBase
{
    private readonly IExcelExportService _excelExportService;

    public ExportsController(IExcelExportService excelExportService)
    {
   _excelExportService = excelExportService;
    }

    /// <summary>
    /// Export exam results to Excel
    /// </summary>
    [HttpGet("exam/{examId}/results")]
    public async Task<IActionResult> ExportExamResults(int examId)
    {
   try
   {
  var excelData = await _excelExportService.ExportExamResultsAsync(examId);
      return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
     $"ExamResults_{examId}_{DateTime.Now:yyyyMMdd}.xlsx");
  }
catch (InvalidOperationException ex)
   {
     return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Export violations to Excel
    /// </summary>
    [HttpGet("exam/{examId}/violations")]
    public async Task<IActionResult> ExportViolations(int examId)
    {
   try
 {
    var excelData = await _excelExportService.ExportViolationsAsync(examId);
    return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
     $"Violations_{examId}_{DateTime.Now:yyyyMMdd}.xlsx");
  }
     catch (InvalidOperationException ex)
        {
   return NotFound(ex.Message);
  }
    }

    /// <summary>
    /// Export grading progress to Excel
    /// </summary>
[HttpGet("exam/{examId}/progress")]
    public async Task<IActionResult> ExportGradingProgress(int examId)
    {
try
   {
   var excelData = await _excelExportService.ExportGradingProgressAsync(examId);
       return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
   $"GradingProgress_{examId}_{DateTime.Now:yyyyMMdd}.xlsx");
        }
  catch (InvalidOperationException ex)
    {
          return NotFound(ex.Message);
  }
    }
}
