using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class DetectedStudentFolder
{
    public long FolderId { get; set; }

    public long BatchId { get; set; }

    public string? StudentCode { get; set; }

    public string FolderPath { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual IngestBatch Batch { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
