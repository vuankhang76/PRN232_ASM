using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ExtractedImage
{
    public long ImageId { get; set; }

    public long SubmissionId { get; set; }

    public long? FromFileId { get; set; }

    public string ImagePath { get; set; } = null!;

    public int? OrderNo { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual SubmissionFile? FromFile { get; set; }

    public virtual Submission Submission { get; set; } = null!;
}
