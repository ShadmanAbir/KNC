using KNC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace KNC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
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

            // Role-Module (many-to-many)
            /*modelBuilder.Entity<RoleModule>()
                .HasKey(rm => new { rm.RoleId, rm.ModuleId });*/

            /*modelBuilder.Entity<RoleModule>()
                .HasRequired(rm => rm.Module)
                .WithMany(m => m.RoleModules)
                .HasForeignKey(rm => rm.ModuleId);*/

            // Program - Course (1 to many)
            modelBuilder.Entity<ProgramCourse>()
                .HasRequired(pc => pc.Program)
                .WithMany(p => p.ProgramCourses)
                .HasForeignKey(pc => pc.ProgramID);

            modelBuilder.Entity<ProgramCourse>()
                .HasRequired(pc => pc.Course)
                .WithMany(c => c.ProgramCourses)
                .HasForeignKey(pc => pc.CourseID);

            // Student - Enrollment (many-to-many via StudentEnrollment)
            modelBuilder.Entity<StudentEnrollment>()
                .HasKey(e => new { e.StudentID, e.ProgramCourseID, e.AcademicYear });

            modelBuilder.Entity<StudentEnrollment>()
                .HasRequired(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID);


            // Student - MonthlyFee (1 to many)
            modelBuilder.Entity<MonthlyFee>()
                .HasRequired(f => f.Student)
                .WithMany(s => s.MonthlyFees)
                .HasForeignKey(f => f.StudentID);

            // Student - StudentFee (1 to many)
            modelBuilder.Entity<StudentFee>()
                .HasRequired(f => f.Student)
                .WithMany(s => s.StudentFees)
                .HasForeignKey(f => f.StudentID);

            // Student - Marks (many-to-many via StudentCourseMark)
            modelBuilder.Entity<Mark>()
                .HasKey(m => new { m.StudentID, m.ProgramCourseID });

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.Student)
                .WithMany(s => s.Marks)
                .HasForeignKey(m => m.StudentID);

            modelBuilder.Entity<Mark>()
                .HasRequired(m => m.ProgramCourse)
                .WithMany(c => c.Marks)
                .HasForeignKey(m => m.ProgramCourseID);

            /*// Student - Attendance (1 to many)
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId);*/

            // Routine - Course + Teacher + Room (1-to-1s/manys)
            modelBuilder.Entity<Routine>()
                .HasRequired(r => r.ProgramCourse)
                .WithMany(c => c.Routines)
                .HasForeignKey(r => r.ProgramCourseID);

            modelBuilder.Entity<Routine>()
                .HasRequired(r => r.Faculty)
                .WithMany(t => t.Routines)
                .HasForeignKey(r => r.FacultyID);

/*            modelBuilder.Entity<Routine>()
                .HasRequired(r => r.Room)
                .WithMany(room => room.Routines)
                .HasForeignKey(r => r.RoomId);*/
/*
            // MarkEntryHistory - Mark (1 to many)
            modelBuilder.Entity<MarkEntryHistory>()
                .HasRequired(h => h.Mark)
                .WithMany(m => m.History)
                .HasForeignKey(h => new { h.StudentId, h.CourseId });

            // Notification - User (optional)
            modelBuilder.Entity<Notification>()
                .HasOptional(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId);

            // Document - UploadedBy (User)
            modelBuilder.Entity<Document>()
                .HasOptional(d => d.UploadedByUser)
                .WithMany()
                .HasForeignKey(d => d.UploadedBy);

            // AuditLog - User (optional)
            modelBuilder.Entity<AuditLog>()
                .HasOptional(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);*/

            // FeeStructure - Program (1 to many)
            modelBuilder.Entity<FeeStructure>()
                .HasRequired(f => f.Programs)
                .WithMany(p => p.FeeStructures)
                .HasForeignKey(f => f.ProgramID);
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
