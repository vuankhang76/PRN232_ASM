using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Submission
{
    public long SubmissionId { get; set; }

    public int ExamId { get; set; }

    public int StudentId { get; set; }

    public long? BatchId { get; set; }

    public long? RootFolderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public decimal? FinalScore { get; set; }

    public string? FinalComment { get; set; }

    public bool IsZeroScore { get; set; }

    public virtual IngestBatch? Batch { get; set; }

    public virtual ICollection<DuplicateGroupMember> DuplicateGroupMembers { get; set; } = new List<DuplicateGroupMember>();

    public virtual Exam Exam { get; set; } = null!;

    public virtual ICollection<ExaminerAssignment> ExaminerAssignments { get; set; } = new List<ExaminerAssignment>();

    public virtual ICollection<ExtractedImage> ExtractedImages { get; set; } = new List<ExtractedImage>();

    public virtual FinalGrade? FinalGrade { get; set; }

    public virtual DetectedStudentFolder? RootFolder { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<SubmissionFile> SubmissionFiles { get; set; } = new List<SubmissionFile>();

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();

    public virtual ICollection<ZeroScoreVerification> ZeroScoreVerifications { get; set; } = new List<ZeroScoreVerification>();
}
