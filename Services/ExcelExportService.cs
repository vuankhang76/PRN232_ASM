using BusinessObjects;
using ClosedXML.Excel;
using Repositories;

namespace Services;

/// <summary>
/// Excel export service implementation
/// </summary>
public class ExcelExportService : IExcelExportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExcelExportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<byte[]> ExportExamResultsAsync(int examId)
    {
 var exam = await _unitOfWork.ExamRepository.GetExamWithDetailsAsync(examId);
        if (exam == null)
throw new InvalidOperationException($"Exam with ID '{examId}' not found.");

  using var workbook = new XLWorkbook();
  var worksheet = workbook.Worksheets.Add("Exam Results");

        // Headers
        worksheet.Cell(1, 1).Value = "Student Code";
        worksheet.Cell(1, 2).Value = "Full Name";
      worksheet.Cell(1, 3).Value = "Email";
    worksheet.Cell(1, 4).Value = "Status";
        worksheet.Cell(1, 5).Value = "Final Score";
   worksheet.Cell(1, 6).Value = "Final Comment";
        worksheet.Cell(1, 7).Value = "Is Zero Score";
        worksheet.Cell(1, 8).Value = "Uploaded At";

   // Style headers
      var headerRange = worksheet.Range("A1:H1");
   headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

  // Data
     int row = 2;
        foreach (var submission in exam.Submissions.OrderBy(s => s.Student.StudentCode))
      {
     worksheet.Cell(row, 1).Value = submission.Student.StudentCode;
worksheet.Cell(row, 2).Value = submission.Student.FullName;
   worksheet.Cell(row, 3).Value = submission.Student.Email ?? "";
   worksheet.Cell(row, 4).Value = submission.Status;
      worksheet.Cell(row, 5).Value = submission.FinalScore ?? 0;
  worksheet.Cell(row, 6).Value = submission.FinalComment ?? "";
     worksheet.Cell(row, 7).Value = submission.IsZeroScore ? "Yes" : "No";
  worksheet.Cell(row, 8).Value = submission.UploadedAt.ToString("yyyy-MM-dd HH:mm:ss");

            row++;
  }

  // Auto-fit columns
     worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
 workbook.SaveAs(stream);
 return stream.ToArray();
  }

    public async Task<byte[]> ExportViolationsAsync(int examId)
    {
   var violations = await _unitOfWork.ViolationRepository.GetByExamIdAsync(examId);

        using var workbook = new XLWorkbook();
  var worksheet = workbook.Worksheets.Add("Violations");

     // Headers
   worksheet.Cell(1, 1).Value = "Student Code";
        worksheet.Cell(1, 2).Value = "Student Name";
   worksheet.Cell(1, 3).Value = "Violation Type";
        worksheet.Cell(1, 4).Value = "Message";
   worksheet.Cell(1, 5).Value = "Severity";
   worksheet.Cell(1, 6).Value = "Evidence";
 worksheet.Cell(1, 7).Value = "Created At";

        // Style headers
  var headerRange = worksheet.Range("A1:G1");
  headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightCoral;

  // Data
     int row = 2;
  foreach (var violation in violations.OrderByDescending(v => v.Severity))
   {
      worksheet.Cell(row, 1).Value = violation.Submission.Student.StudentCode;
   worksheet.Cell(row, 2).Value = violation.Submission.Student.FullName;
 worksheet.Cell(row, 3).Value = violation.ViolationType.Name;
 worksheet.Cell(row, 4).Value = violation.Message;
  worksheet.Cell(row, 5).Value = violation.Severity;
            worksheet.Cell(row, 6).Value = violation.Evidence ?? "";
    worksheet.Cell(row, 7).Value = violation.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");

row++;
  }

   // Auto-fit columns
        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
      return stream.ToArray();
    }

    public async Task<byte[]> ExportGradingProgressAsync(int examId)
    {
      var exam = await _unitOfWork.ExamRepository.GetExamWithDetailsAsync(examId);
        if (exam == null)
  throw new InvalidOperationException($"Exam with ID '{examId}' not found.");

   var assignments = await _unitOfWork.ExaminerAssignmentRepository.GetByExamIdAsync(examId);

  using var workbook = new XLWorkbook();
   var worksheet = workbook.Worksheets.Add("Grading Progress");

        // Headers
        worksheet.Cell(1, 1).Value = "Student Code";
  worksheet.Cell(1, 2).Value = "Student Name";
worksheet.Cell(1, 3).Value = "Examiner";
  worksheet.Cell(1, 4).Value = "Status";
        worksheet.Cell(1, 5).Value = "Assigned At";
   worksheet.Cell(1, 6).Value = "Scores Count";

   // Style headers
        var headerRange = worksheet.Range("A1:F1");
  headerRange.Style.Font.Bold = true;
 headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;

      // Data
  int row = 2;
      foreach (var assignment in assignments.OrderBy(a => a.Submission.Student.StudentCode))
        {
    worksheet.Cell(row, 1).Value = assignment.Submission.Student.StudentCode;
   worksheet.Cell(row, 2).Value = assignment.Submission.Student.FullName;
  worksheet.Cell(row, 3).Value = assignment.Examiner.FullName;
     worksheet.Cell(row, 4).Value = assignment.Status;
    worksheet.Cell(row, 5).Value = assignment.AssignedAt.ToString("yyyy-MM-dd HH:mm:ss");
       worksheet.Cell(row, 6).Value = assignment.ExaminerScores.Count;

          row++;
        }

  // Auto-fit columns
 worksheet.Columns().AdjustToContents();

     using var stream = new MemoryStream();
        workbook.SaveAs(stream);
 return stream.ToArray();
    }
}
