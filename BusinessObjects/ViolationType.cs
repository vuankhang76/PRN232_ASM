using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ViolationType
{
    public int ViolationTypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
