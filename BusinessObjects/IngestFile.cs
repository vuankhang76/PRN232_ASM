using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class IngestFile
{
    public long IngestFileId { get; set; }

    public long BatchId { get; set; }

    public string RelativePath { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string? Ext { get; set; }

    public long? SizeBytes { get; set; }

    public string? DetectedType { get; set; }

    public byte[]? Sha256 { get; set; }

    public bool IsArchive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual IngestBatch Batch { get; set; } = null!;

    public virtual ICollection<SubmissionFile> SubmissionFiles { get; set; } = new List<SubmissionFile>();
}
