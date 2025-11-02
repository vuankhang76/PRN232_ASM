using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentCode { get; set; } = null!;

    public string? FullName { get; set; }

    public string? ClassName { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
