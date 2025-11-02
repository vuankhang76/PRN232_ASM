using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Exam
{
    public int ExamId { get; set; }

    public int SubjectId { get; set; }

    public int SemesterId { get; set; }

    public string ExamCode { get; set; } = null!;

    public string ExamName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ExamPaperPath { get; set; }

    public string? MarkingSheetPath { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DuplicateGroup> DuplicateGroups { get; set; } = new List<DuplicateGroup>();

    public virtual ICollection<IngestBatch> IngestBatches { get; set; } = new List<IngestBatch>();

    public virtual ICollection<RubricItem> RubricItems { get; set; } = new List<RubricItem>();

    public virtual Semester Semester { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
