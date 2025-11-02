using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class DuplicateGroup
{
    public long GroupId { get; set; }

    public int ExamId { get; set; }

    public decimal Similarity { get; set; }

    public string? ReportPath { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DuplicateGroupMember> DuplicateGroupMembers { get; set; } = new List<DuplicateGroupMember>();

    public virtual Exam Exam { get; set; } = null!;
}
