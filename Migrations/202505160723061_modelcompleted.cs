namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelcompleted : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgramCourses",
                c => new
                    {
                        ProgramCourseID = c.Int(nullable: false, identity: true),
                        ProgramID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Section = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        StudentEnrollment_StudentID = c.Int(),
                        StudentEnrollment_ProgramCourseID = c.Int(),
                        StudentEnrollment_AcademicYear = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProgramCourseID)
                .ForeignKey("dbo.StudentEnrollments", t => new { t.StudentEnrollment_StudentID, t.StudentEnrollment_ProgramCourseID, t.StudentEnrollment_AcademicYear })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.EducationPrograms", t => t.ProgramID, cascadeDelete: true)
                .Index(t => t.ProgramID)
                .Index(t => t.CourseID)
                .Index(t => new { t.StudentEnrollment_StudentID, t.StudentEnrollment_ProgramCourseID, t.StudentEnrollment_AcademicYear });
            
            CreateTable(
                "dbo.CourseTeacherAssignments",
                c => new
                    {
                        AssignmentID = c.Int(nullable: false, identity: true),
                        ProgramCourseID = c.Int(nullable: false),
                        FacultyID = c.Int(nullable: false),
                        PreferredDays = c.String(),
                        PreferredTimeSlot = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentID)
                .ForeignKey("dbo.Faculties", t => t.FacultyID, cascadeDelete: true)
                .ForeignKey("dbo.ProgramCourses", t => t.ProgramCourseID, cascadeDelete: true)
                .Index(t => t.ProgramCourseID)
                .Index(t => t.FacultyID);
            
            CreateTable(
                "dbo.Routines",
                c => new
                    {
                        RoutineID = c.Int(nullable: false, identity: true),
                        ProgramID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Section = c.String(),
                        ProgramCourseID = c.Int(nullable: false),
                        FacultyID = c.Int(nullable: false),
                        RoomNo = c.String(),
                        DayOfWeek = c.String(),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Program_EducationProgramID = c.Int(),
                    })
                .PrimaryKey(t => t.RoutineID)
                .ForeignKey("dbo.Faculties", t => t.FacultyID, cascadeDelete: true)
                .ForeignKey("dbo.EducationPrograms", t => t.Program_EducationProgramID)
                .ForeignKey("dbo.ProgramCourses", t => t.ProgramCourseID, cascadeDelete: true)
                .Index(t => t.ProgramCourseID)
                .Index(t => t.FacultyID)
                .Index(t => t.Program_EducationProgramID);
            
            CreateTable(
                "dbo.FeeStructures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProgramID = c.Int(nullable: false),
                        FeeType = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Frequency = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EducationPrograms", t => t.ProgramID, cascadeDelete: true)
                .Index(t => t.ProgramID);
            
            CreateTable(
                "dbo.StudentEnrollments",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        ProgramCourseID = c.Int(nullable: false),
                        AcademicYear = c.String(nullable: false, maxLength: 128),
                        EnrollmentID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        EnrollmentStatus = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        DropDate = c.DateTime(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => new { t.StudentID, t.ProgramCourseID, t.AcademicYear })
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        ProgramCourseID = c.Int(nullable: false),
                        MarkID = c.Int(nullable: false),
                        ExamName = c.String(),
                        MarksObtained = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMarks = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Grade = c.String(),
                        ExamDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.ProgramCourseID })
                .ForeignKey("dbo.ProgramCourses", t => t.ProgramCourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.ProgramCourseID);
            
            CreateTable(
                "dbo.MonthlyFees",
                c => new
                    {
                        MonthlyFeeId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        MonthYear = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DueDate = c.DateTime(nullable: false),
                        PaidDate = c.DateTime(),
                        PaymentStatus = c.String(),
                        PaymentMethod = c.String(),
                        Notes = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MonthlyFeeId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentFees",
                c => new
                    {
                        FeeID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        FeeType = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.FeeID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            AddColumn("dbo.Courses", "CourseCode", c => c.String());
            AddColumn("dbo.Faculties", "DesignationID", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "Course_CourseID", c => c.Int());
            AddColumn("dbo.Students", "ProgramID", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Program_EducationProgramID", c => c.Int());
            CreateIndex("dbo.Faculties", "Course_CourseID");
            CreateIndex("dbo.Students", "Program_EducationProgramID");
            AddForeignKey("dbo.Faculties", "Course_CourseID", "dbo.Courses", "CourseID");
            AddForeignKey("dbo.Students", "Program_EducationProgramID", "dbo.EducationPrograms", "EducationProgramID");
            DropColumn("dbo.Courses", "Fee");
            DropColumn("dbo.Courses", "Duration");
            DropColumn("dbo.Faculties", "Designation");
            DropColumn("dbo.Faculties", "TeachingExperience");
            DropColumn("dbo.Faculties", "ClinicalExperience");
            DropColumn("dbo.Faculties", "LocalPublication");
            DropColumn("dbo.Faculties", "InternationalPublication");
            DropColumn("dbo.Students", "Program");
            DropTable("dbo.Certificates");
            DropTable("dbo.Designations");
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.Sessions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionID = c.Int(nullable: false, identity: true),
                        SessionCode = c.String(),
                        Program = c.Int(nullable: false),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionID);
            
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentID);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationID = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DesignationID);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        CertificateID = c.Int(nullable: false, identity: true),
                        HolderID = c.Int(nullable: false),
                        CertificateName = c.String(),
                        HolderCode = c.String(),
                        Location = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CertificateID);
            
            AddColumn("dbo.Students", "Program", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "InternationalPublication", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "LocalPublication", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "ClinicalExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "TeachingExperience", c => c.Int(nullable: false));
            AddColumn("dbo.Faculties", "Designation", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "Duration", c => c.String());
            AddColumn("dbo.Courses", "Fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.ProgramCourses", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.ProgramCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.CourseTeacherAssignments", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.Routines", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.StudentFees", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "Program_EducationProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.MonthlyFees", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Marks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Marks", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.StudentEnrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.ProgramCourses", new[] { "StudentEnrollment_StudentID", "StudentEnrollment_ProgramCourseID", "StudentEnrollment_AcademicYear" }, "dbo.StudentEnrollments");
            DropForeignKey("dbo.Routines", "Program_EducationProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.FeeStructures", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.Routines", "FacultyID", "dbo.Faculties");
            DropForeignKey("dbo.Faculties", "Course_CourseID", "dbo.Courses");
            DropForeignKey("dbo.CourseTeacherAssignments", "FacultyID", "dbo.Faculties");
            DropIndex("dbo.StudentFees", new[] { "StudentID" });
            DropIndex("dbo.MonthlyFees", new[] { "StudentId" });
            DropIndex("dbo.Marks", new[] { "ProgramCourseID" });
            DropIndex("dbo.Marks", new[] { "StudentID" });
            DropIndex("dbo.StudentEnrollments", new[] { "StudentID" });
            DropIndex("dbo.Students", new[] { "Program_EducationProgramID" });
            DropIndex("dbo.FeeStructures", new[] { "ProgramID" });
            DropIndex("dbo.Routines", new[] { "Program_EducationProgramID" });
            DropIndex("dbo.Routines", new[] { "FacultyID" });
            DropIndex("dbo.Routines", new[] { "ProgramCourseID" });
            DropIndex("dbo.Faculties", new[] { "Course_CourseID" });
            DropIndex("dbo.CourseTeacherAssignments", new[] { "FacultyID" });
            DropIndex("dbo.CourseTeacherAssignments", new[] { "ProgramCourseID" });
            DropIndex("dbo.ProgramCourses", new[] { "StudentEnrollment_StudentID", "StudentEnrollment_ProgramCourseID", "StudentEnrollment_AcademicYear" });
            DropIndex("dbo.ProgramCourses", new[] { "CourseID" });
            DropIndex("dbo.ProgramCourses", new[] { "ProgramID" });
            DropColumn("dbo.Students", "Program_EducationProgramID");
            DropColumn("dbo.Students", "ProgramID");
            DropColumn("dbo.Faculties", "Course_CourseID");
            DropColumn("dbo.Faculties", "DesignationID");
            DropColumn("dbo.Courses", "CourseCode");
            DropTable("dbo.StudentFees");
            DropTable("dbo.MonthlyFees");
            DropTable("dbo.Marks");
            DropTable("dbo.StudentEnrollments");
            DropTable("dbo.FeeStructures");
            DropTable("dbo.Routines");
            DropTable("dbo.CourseTeacherAssignments");
            DropTable("dbo.ProgramCourses");
        }
    }
}
