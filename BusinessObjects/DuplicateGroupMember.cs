using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class DuplicateGroupMember
{
    public long GroupId { get; set; }

    public long SubmissionId { get; set; }

    public decimal? Similarity { get; set; }

    public virtual DuplicateGroup Group { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;
}
