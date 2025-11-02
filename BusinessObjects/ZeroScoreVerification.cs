using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ZeroScoreVerification
{
    public long VerificationId { get; set; }

    public long SubmissionId { get; set; }

    public Guid ModeratorId { get; set; }

    public string? Reason { get; set; }

    public DateTime VerifiedAt { get; set; }

    public virtual AppUser Moderator { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;
}
