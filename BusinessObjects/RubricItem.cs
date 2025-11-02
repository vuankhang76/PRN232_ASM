using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class RubricItem
{
    public int RubricItemId { get; set; }

    public int ExamId { get; set; }

    public int OrderNo { get; set; }

    public string? Code { get; set; }

    public string Title { get; set; } = null!;

    public string? Keywords { get; set; }

    public decimal MaxPoint { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual ICollection<ExaminerScore> ExaminerScores { get; set; } = new List<ExaminerScore>();
}
