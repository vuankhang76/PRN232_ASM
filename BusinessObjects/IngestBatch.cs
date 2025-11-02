using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class IngestBatch
{
    public long BatchId { get; set; }

    public int ExamId { get; set; }

    public Guid? UploadedBy { get; set; }

    public string SourceArchivePath { get; set; } = null!;

    public string? ExtractRootPath { get; set; }

    public string Status { get; set; } = null!;

    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DetectedStudentFolder> DetectedStudentFolders { get; set; } = new List<DetectedStudentFolder>();

    public virtual Exam Exam { get; set; } = null!;

    public virtual ICollection<IngestFile> IngestFiles { get; set; } = new List<IngestFile>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
