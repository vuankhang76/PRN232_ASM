using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class AppUser
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ExaminerAssignment> ExaminerAssignmentAssignedByNavigations { get; set; } = new List<ExaminerAssignment>();

    public virtual ICollection<ExaminerAssignment> ExaminerAssignmentExaminers { get; set; } = new List<ExaminerAssignment>();

    public virtual ICollection<ZeroScoreVerification> ZeroScoreVerifications { get; set; } = new List<ZeroScoreVerification>();

    public virtual ICollection<AppRole> Roles { get; set; } = new List<AppRole>();
}
