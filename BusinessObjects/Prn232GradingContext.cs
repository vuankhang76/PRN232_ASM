using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Prn232GradingContext : DbContext
{
    public Prn232GradingContext()
    {
    }

    public Prn232GradingContext(DbContextOptions<Prn232GradingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppRole> AppRoles { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<DetectedStudentFolder> DetectedStudentFolders { get; set; }

    public virtual DbSet<DuplicateGroup> DuplicateGroups { get; set; }

    public virtual DbSet<DuplicateGroupMember> DuplicateGroupMembers { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExaminerAssignment> ExaminerAssignments { get; set; }

    public virtual DbSet<ExaminerScore> ExaminerScores { get; set; }

    public virtual DbSet<ExtractedImage> ExtractedImages { get; set; }

    public virtual DbSet<FinalGrade> FinalGrades { get; set; }

    public virtual DbSet<IngestBatch> IngestBatches { get; set; }

    public virtual DbSet<IngestFile> IngestFiles { get; set; }

    public virtual DbSet<RubricItem> RubricItems { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionFile> SubmissionFiles { get; set; }

    public virtual DbSet<Violation> Violations { get; set; }

    public virtual DbSet<ViolationType> ViolationTypes { get; set; }

    public virtual DbSet<ZeroScoreVerification> ZeroScoreVerifications { get; set; }

    private String GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        string connectionString = config["ConnectionStrings:DefaultConnection"];
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__AppRoles__8AFACE1A5F9B2F1D");

            entity.HasIndex(e => e.RoleName, "UQ__AppRoles__8A2B6160FAF03C42").IsUnique();

            entity.Property(e => e.RoleName)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__AppUsers__1788CC4CA1F60BBA");

            entity.HasIndex(e => e.Username, "UQ__AppUsers__536C85E4161786A0").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(128);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<AppRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__412EB0B6"),
                    l => l.HasOne<AppUser>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__403A8C7D"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD38FE83E6");
                        j.ToTable("UserRoles");
                    });
        });

        modelBuilder.Entity<DetectedStudentFolder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__Detected__ACD7107F8B9F1BBF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FolderPath).HasMaxLength(400);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasOne(d => d.Batch).WithMany(p => p.DetectedStudentFolders)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetectedS__Batch__5DCAEF64");
        });

        modelBuilder.Entity<DuplicateGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Duplicat__149AF36AE703B26B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ReportPath).HasMaxLength(400);
            entity.Property(e => e.Similarity).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Exam).WithMany(p => p.DuplicateGroups)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duplicate__ExamI__00200768");
        });

        modelBuilder.Entity<DuplicateGroupMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.SubmissionId }).HasName("PK__Duplicat__50D31D78ECE6D518");

            entity.Property(e => e.Similarity).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Group).WithMany(p => p.DuplicateGroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duplicate__Group__02FC7413");

            entity.HasOne(d => d.Submission).WithMany(p => p.DuplicateGroupMembers)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Duplicate__Submi__03F0984C");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exams__297521C7BC80BE0F");

            entity.HasIndex(e => new { e.SubjectId, e.SemesterId, e.ExamCode }, "UQ__Exams__A3A72A4286BE4B74").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.ExamCode)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ExamName).HasMaxLength(128);
            entity.Property(e => e.ExamPaperPath).HasMaxLength(400);
            entity.Property(e => e.MarkingSheetPath).HasMaxLength(400);

            entity.HasOne(d => d.Semester).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__SemesterI__4D94879B");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Exams__SubjectId__4CA06362");
        });

        modelBuilder.Entity<ExaminerAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Examiner__32499E7731FE0AE4");

            entity.Property(e => e.AssignedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValue("ASSIGNED");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.ExaminerAssignmentAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .HasConstraintName("FK__ExaminerA__Assig__0A9D95DB");

            entity.HasOne(d => d.Examiner).WithMany(p => p.ExaminerAssignmentExaminers)
                .HasForeignKey(d => d.ExaminerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExaminerA__Exami__09A971A2");

            entity.HasOne(d => d.Submission).WithMany(p => p.ExaminerAssignments)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExaminerA__Submi__08B54D69");
        });

        modelBuilder.Entity<ExaminerScore>(entity =>
        {
            entity.HasKey(e => e.ExaminerScoreId).HasName("PK__Examiner__9ECF6010A61D36F2");

            entity.HasIndex(e => new { e.AssignmentId, e.RubricItemId }, "UQ_Assignment_Rubric").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.Score).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.ScoredAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Assignment).WithMany(p => p.ExaminerScores)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExaminerS__Assig__10566F31");

            entity.HasOne(d => d.RubricItem).WithMany(p => p.ExaminerScores)
                .HasForeignKey(d => d.RubricItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExaminerS__Rubri__114A936A");
        });

        modelBuilder.Entity<ExtractedImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Extracte__7516F70C0C4AA5E0");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ImagePath).HasMaxLength(400);

            entity.HasOne(d => d.FromFile).WithMany(p => p.ExtractedImages)
                .HasForeignKey(d => d.FromFileId)
                .HasConstraintName("FK__Extracted__FromF__73BA3083");

            entity.HasOne(d => d.Submission).WithMany(p => p.ExtractedImages)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Extracted__Submi__72C60C4A");
        });

        modelBuilder.Entity<FinalGrade>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__FinalGra__449EE125E2FF57CE");

            entity.Property(e => e.SubmissionId).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.DecidedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FinalScore).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Method)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValue("AVERAGE");

            entity.HasOne(d => d.Submission).WithOne(p => p.FinalGrade)
                .HasForeignKey<FinalGrade>(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FinalGrad__Submi__160F4887");
        });

        modelBuilder.Entity<IngestBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__IngestBa__5D55CE5834770EA8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ExtractRootPath).HasMaxLength(400);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.SourceArchivePath).HasMaxLength(400);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("UPLOADED");

            entity.HasOne(d => d.Exam).WithMany(p => p.IngestBatches)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IngestBat__ExamI__5535A963");
        });

        modelBuilder.Entity<IngestFile>(entity =>
        {
            entity.HasKey(e => e.IngestFileId).HasName("PK__IngestFi__6E31C5A12A06EBE9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.DetectedType)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Ext)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.RelativePath).HasMaxLength(400);
            entity.Property(e => e.Sha256).HasMaxLength(32);

            entity.HasOne(d => d.Batch).WithMany(p => p.IngestFiles)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IngestFil__Batch__59FA5E80");
        });

        modelBuilder.Entity<RubricItem>(entity =>
        {
            entity.HasKey(e => e.RubricItemId).HasName("PK__RubricIt__811E77C119FAB620");

            entity.Property(e => e.Code)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Keywords).HasMaxLength(1024);
            entity.Property(e => e.MaxPoint).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Title).HasMaxLength(256);

            entity.HasOne(d => d.Exam).WithMany(p => p.RubricItems)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RubricIte__ExamI__5070F446");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("PK__Semester__043301DD34A3445A");

            entity.HasIndex(e => e.Code, "UQ__Semester__A25C5AA7FD0C0A9F").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99273C12BD");

            entity.HasIndex(e => e.StudentCode, "UQ__Students__1FC88604DDC8A265").IsUnique();

            entity.Property(e => e.ClassName).HasMaxLength(64);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(128);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A838372DD4");

            entity.HasIndex(e => e.Code, "UQ__Subjects__A25C5AA7067BC270").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE125FD36134B");

            entity.HasIndex(e => new { e.ExamId, e.StudentId }, "UQ_Submission").IsUnique();

            entity.Property(e => e.FinalComment).HasMaxLength(2000);
            entity.Property(e => e.FinalScore).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasDefaultValue("UPLOADED");
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Batch).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK__Submissio__Batch__6A30C649");

            entity.HasOne(d => d.Exam).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Submissio__ExamI__68487DD7");

            entity.HasOne(d => d.RootFolder).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.RootFolderId)
                .HasConstraintName("FK__Submissio__RootF__6B24EA82");

            entity.HasOne(d => d.Student).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Submissio__Stude__693CA210");
        });

        modelBuilder.Entity<SubmissionFile>(entity =>
        {
            entity.HasKey(e => e.SubmissionFileId).HasName("PK__Submissi__3564E69B724A8069");

            entity.Property(e => e.LogicalRole)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.IngestFile).WithMany(p => p.SubmissionFiles)
                .HasForeignKey(d => d.IngestFileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Submissio__Inges__6EF57B66");

            entity.HasOne(d => d.Submission).WithMany(p => p.SubmissionFiles)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Submissio__Submi__6E01572D");
        });

        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("PK__Violatio__18B6DC08C33062AD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Evidence).HasMaxLength(400);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.Severity).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Submission).WithMany(p => p.Violations)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__Submi__7B5B524B");

            entity.HasOne(d => d.ViolationType).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ViolationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__Viola__7C4F7684");
        });

        modelBuilder.Entity<ViolationType>(entity =>
        {
            entity.HasKey(e => e.ViolationTypeId).HasName("PK__Violatio__3B1A4D1D96CF1115");

            entity.HasIndex(e => e.Code, "UQ__Violatio__A25C5AA7A34BD076").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<ZeroScoreVerification>(entity =>
        {
            entity.HasKey(e => e.VerificationId).HasName("PK__ZeroScor__306D4907A3C8060A");

            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.VerifiedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Moderator).WithMany(p => p.ZeroScoreVerifications)
                .HasForeignKey(d => d.ModeratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ZeroScore__Moder__1AD3FDA4");

            entity.HasOne(d => d.Submission).WithMany(p => p.ZeroScoreVerifications)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ZeroScore__Submi__19DFD96B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
