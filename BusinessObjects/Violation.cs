using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Violation
{
    public long ViolationId { get; set; }

    public long SubmissionId { get; set; }

    public int ViolationTypeId { get; set; }

    public byte Severity { get; set; }

    public string? Message { get; set; }

    public string? Evidence { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Submission Submission { get; set; } = null!;

    public virtual ViolationType ViolationType { get; set; } = null!;
}
