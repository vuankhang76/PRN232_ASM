using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class FinalGrade
{
    public long SubmissionId { get; set; }

    public decimal FinalScore { get; set; }

    public string Method { get; set; } = null!;

    public Guid? DecidedBy { get; set; }

    public DateTime DecidedAt { get; set; }

    public string? Comment { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
