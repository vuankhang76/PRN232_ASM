using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ExaminerAssignment
{
    public long AssignmentId { get; set; }

    public long SubmissionId { get; set; }

    public Guid ExaminerId { get; set; }

    public Guid? AssignedBy { get; set; }

    public DateTime AssignedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual AppUser? AssignedByNavigation { get; set; }

    public virtual AppUser Examiner { get; set; } = null!;

    public virtual ICollection<ExaminerScore> ExaminerScores { get; set; } = new List<ExaminerScore>();

    public virtual Submission Submission { get; set; } = null!;
}
