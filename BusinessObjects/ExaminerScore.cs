using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ExaminerScore
{
    public long ExaminerScoreId { get; set; }

    public long AssignmentId { get; set; }

    public int RubricItemId { get; set; }

    public decimal Score { get; set; }

    public string? Comment { get; set; }

    public DateTime ScoredAt { get; set; }

    public virtual ExaminerAssignment Assignment { get; set; } = null!;

    public virtual RubricItem RubricItem { get; set; } = null!;
}
