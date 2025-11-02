using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
