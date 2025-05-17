using KNC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace KNC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            // Performance optimizations
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            
            // Optimize memory usage
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity Framework Defaults
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers");

            // Program - Course (1 to many)
            modelBuilder.Entity<ProgramCourse>()
                .HasRequired(pc => pc.Program)
                .WithMany(p => p.ProgramCourses)
                .HasForeignKey(pc => pc.ProgramID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramCourse>()
                .HasRequired(pc => pc.Course)
                .WithMany(c => c.ProgramCourses)
                .HasForeignKey(pc => pc.CourseID)
                .WillCascadeOnDelete(false);

            // Student - Enrollment (many-to-many via StudentEnrollment)
            modelBuilder.Entity<StudentEnrollment>()
                .HasKey(e => new { e.StudentID, e.ProgramCourseID, e.AcademicYear });

            modelBuilder.Entity<StudentEnrollment>()
                .HasRequired(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            // Student - MonthlyFee (1 to many)
            modelBuilder.Entity<MonthlyFee>()
                .HasRequired(f => f.Student)
                .WithMany(s => s.MonthlyFees)
                .HasForeignKey(f => f.StudentID)
                .WillCascadeOnDelete(false);

            // Student - StudentFee (1 to many)
            modelBuilder.Entity<StudentFee>()
                .HasRequired(f => f.Student)
                .WithMany(s => s.StudentFees)
                .HasForeignKey(f => f.StudentID)
                .WillCascadeOnDelete(false);

            // Student - Marks (many-to-many via StudentCourseMark)
            modelBuilder.Entity<Mark>()
                .HasKey(m => m.MarkID);

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.Student)
                .WithMany(s => s.Marks)
                .HasForeignKey(m => m.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.ProgramCourse)
                .WithMany(c => c.Marks)
                .HasForeignKey(m => m.ProgramCourseID)
                .WillCascadeOnDelete(false);

            // Routine - Course + Teacher + Room (1-to-1s/manys)
            modelBuilder.Entity<Routine>()
                .HasRequired(r => r.ProgramCourse)
                .WithMany(c => c.Routines)
                .HasForeignKey(r => r.ProgramCourseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Routine>()
                .HasRequired(r => r.Faculty)
                .WithMany(t => t.Routines)
                .HasForeignKey(r => r.FacultyID)
                .WillCascadeOnDelete(false);

            // FeeStructure - Program (1 to many)
            modelBuilder.Entity<FeeStructure>()
                .HasRequired(f => f.Programs)
                .WithMany(p => p.FeeStructures)
                .HasForeignKey(f => f.ProgramID)
                .WillCascadeOnDelete(false);

            // Create indexes
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.IsDeleted)
                .HasName("IX_Students_IsDeleted");

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.ProgramID)
                .HasName("IX_Students_ProgramID");

            // Add additional performance-focused indexes
            modelBuilder.Entity<Mark>()
                .HasIndex(m => new { m.StudentID, m.ProgramCourseID })
                .HasName("IX_Marks_Student_ProgramCourse");

            modelBuilder.Entity<MonthlyFee>()
                .HasIndex(f => new { f.StudentID, f.MonthYear })
                .HasName("IX_MonthlyFees_Student_MonthYear");

            modelBuilder.Entity<StudentEnrollment>()
                .HasIndex(e => new { e.StudentID, e.AcademicYear })
                .HasName("IX_StudentEnrollments_Student_Year");

            modelBuilder.Entity<ProgramCourse>()
                .HasIndex(pc => new { pc.ProgramID, pc.CourseID })
                .HasName("IX_ProgramCourses_Program_Course");

            modelBuilder.Entity<Routine>()
                .HasIndex(r => new { r.ProgramID, r.Year, r.Section })
                .HasName("IX_Routines_Program_Year_Section");

            
        }

        // Replace the problematic code in SaveChangesAsync method
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Database.ExecuteSqlCommandAsync("SET NOCOUNT ON;", cancellationToken)
                .ContinueWith(_ => base.SaveChangesAsync(cancellationToken))
                .Unwrap();
        }

        // Add bulk operations support
        public async Task BulkInsertAsync<T>(IList<T> entities) where T : class
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    this.Set<T>().AddRange(entities);
                    await SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Students and Teachers
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<ProgramCourse> ProgramCourses { get; set; }
        public DbSet<Designation> Designations { get; set; } // For future-proofing


        // Programs, Courses, Enrollments, Results
        public DbSet<EducationPrograms> EducationPrograms { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public DbSet<Mark> Marks { get; set; }

        // Routine Management
        public DbSet<Routine> Routines { get; set; }
        //public DbSet<Room> Rooms { get; set; }
        public DbSet<CourseTeacherAssignment> CourseTeacherAssignments { get; set; }
        // Payments
        public DbSet<MonthlyFee> MonthlyFees { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }


        // Roles & Modules (RBAC)
        //public DbSet<Module> Modules { get; set; }
        //public DbSet<RoleModule> RoleModules { get; set; }

        // Supporting Features
/*        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }*/

        // Extra (Optional Future-proofing)
        public DbSet<FeeStructure> FeeStructures { get; set; }
        //public DbSet<MarkEntryHistory> MarkEntryHistories { get; set; }

    }
}

// Fix for CS1061: 'IQueryable<T>' does not contain a definition for 'TagWith'
// The 'TagWith' method is available in Entity Framework Core, not in Entity Framework 6.
// Since the provided code uses Entity Framework 6, the 'TagWith' method cannot be used.
// The fix is to remove the 'TagWith' call or replace it with a comment or logging mechanism.

public static class DbContextExtensions
{
    public static IQueryable<T> AsNoTrackingWithIdentityResolution<T>(this IQueryable<T> queryable) where T : class
    {
        // Removed 'TagWith' as it is not supported in EF6
        return queryable.AsNoTracking();
    }
}
