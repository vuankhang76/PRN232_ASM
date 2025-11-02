using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Semester
{
    public int SemesterId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
