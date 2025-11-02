using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class SubmissionFile
{
    public long SubmissionFileId { get; set; }

    public long SubmissionId { get; set; }

    public long IngestFileId { get; set; }

    public string? LogicalRole { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<ExtractedImage> ExtractedImages { get; set; } = new List<ExtractedImage>();

    public virtual IngestFile IngestFile { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;
}
