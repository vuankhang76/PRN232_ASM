using Microsoft.AspNetCore.SignalR;

namespace Controllers.Hubs;

/// <summary>
/// SignalR hub for real-time grading notifications
/// </summary>
public class GradingHub : Hub
{
    /// <summary>
    /// Notify when a submission is uploaded
    /// </summary>
    public async Task NotifySubmissionUploaded(int examId, long submissionId, string studentCode)
    {
        await Clients.All.SendAsync("SubmissionUploaded", examId, submissionId, studentCode);
    }

    /// <summary>
    /// Notify when a submission is graded
    /// </summary>
    public async Task NotifySubmissionGraded(long submissionId, string studentCode, decimal score)
    {
        await Clients.All.SendAsync("SubmissionGraded", submissionId, studentCode, score);
    }

    /// <summary>
    /// Notify when a violation is detected
    /// </summary>
    public async Task NotifyViolationDetected(long submissionId, string studentCode, string violationType)
    {
   await Clients.All.SendAsync("ViolationDetected", submissionId, studentCode, violationType);
    }

 /// <summary>
    /// Notify when an examiner is assigned
    /// </summary>
    public async Task NotifyExaminerAssigned(long assignmentId, string examinerName, long submissionId)
    {
        await Clients.All.SendAsync("ExaminerAssigned", assignmentId, examinerName, submissionId);
    }

    /// <summary>
    /// Send grading progress update
    /// </summary>
    public async Task SendGradingProgress(int examId, int totalSubmissions, int gradedSubmissions)
    {
        await Clients.All.SendAsync("GradingProgress", examId, totalSubmissions, gradedSubmissions);
    }
}
