using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class AppRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
}
