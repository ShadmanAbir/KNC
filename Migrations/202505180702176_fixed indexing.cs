namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedindexing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProgramCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Marks", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.ProgramCourses", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.Routines", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.Routines", "FacultyID", "dbo.Faculties");
            DropForeignKey("dbo.FeeStructures", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.StudentEnrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Marks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.MonthlyFees", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentFees", "StudentID", "dbo.Students");
            DropIndex("dbo.ProgramCourses", new[] { "ProgramID" });
            DropIndex("dbo.ProgramCourses", new[] { "CourseID" });
            DropIndex("dbo.StudentEnrollments", new[] { "StudentID" });
            DropIndex("dbo.Marks", new[] { "StudentID" });
            DropIndex("dbo.Marks", new[] { "ProgramCourseID" });
            DropIndex("dbo.MonthlyFees", new[] { "StudentID" });
            DropPrimaryKey("dbo.Marks");
            AlterColumn("dbo.Routines", "Section", c => c.String(maxLength: 7));
            AlterColumn("dbo.Marks", "MarkID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.MonthlyFees", "MonthYear", c => c.String(maxLength: 7));
            AddPrimaryKey("dbo.Marks", "MarkID");
            CreateIndex("dbo.ProgramCourses", new[] { "ProgramID", "CourseID" }, name: "IX_ProgramCourses_Program_Course");
            CreateIndex("dbo.Routines", new[] { "ProgramID", "Year", "Section" }, name: "IX_Routines_Program_Year_Section");
            CreateIndex("dbo.Students", "ProgramID", name: "IX_Students_ProgramID");
            CreateIndex("dbo.Students", "IsDeleted", name: "IX_Students_IsDeleted");
            CreateIndex("dbo.StudentEnrollments", new[] { "StudentID", "AcademicYear" }, name: "IX_StudentEnrollments_Student_Year");
            CreateIndex("dbo.Marks", new[] { "StudentID", "ProgramCourseID" }, name: "IX_Marks_Student_ProgramCourse");
            CreateIndex("dbo.MonthlyFees", new[] { "StudentID", "MonthYear" }, name: "IX_MonthlyFees_Student_MonthYear");
            AddForeignKey("dbo.ProgramCourses", "CourseID", "dbo.Courses", "CourseID");
            AddForeignKey("dbo.Marks", "ProgramCourseID", "dbo.ProgramCourses", "ProgramCourseID");
            AddForeignKey("dbo.ProgramCourses", "ProgramID", "dbo.EducationPrograms", "EducationProgramID");
            AddForeignKey("dbo.Routines", "ProgramCourseID", "dbo.ProgramCourses", "ProgramCourseID");
            AddForeignKey("dbo.Routines", "FacultyID", "dbo.Faculties", "FacultyID");
            AddForeignKey("dbo.FeeStructures", "ProgramID", "dbo.EducationPrograms", "EducationProgramID");
            AddForeignKey("dbo.StudentEnrollments", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.Marks", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.MonthlyFees", "StudentID", "dbo.Students", "StudentID");
            AddForeignKey("dbo.StudentFees", "StudentID", "dbo.Students", "StudentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentFees", "StudentID", "dbo.Students");
            DropForeignKey("dbo.MonthlyFees", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Marks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentEnrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.FeeStructures", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.Routines", "FacultyID", "dbo.Faculties");
            DropForeignKey("dbo.Routines", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.ProgramCourses", "ProgramID", "dbo.EducationPrograms");
            DropForeignKey("dbo.Marks", "ProgramCourseID", "dbo.ProgramCourses");
            DropForeignKey("dbo.ProgramCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.MonthlyFees", "IX_MonthlyFees_Student_MonthYear");
            DropIndex("dbo.Marks", "IX_Marks_Student_ProgramCourse");
            DropIndex("dbo.StudentEnrollments", "IX_StudentEnrollments_Student_Year");
            DropIndex("dbo.Students", "IX_Students_IsDeleted");
            DropIndex("dbo.Students", "IX_Students_ProgramID");
            DropIndex("dbo.Routines", "IX_Routines_Program_Year_Section");
            DropIndex("dbo.ProgramCourses", "IX_ProgramCourses_Program_Course");
            DropPrimaryKey("dbo.Marks");
            AlterColumn("dbo.MonthlyFees", "MonthYear", c => c.String());
            AlterColumn("dbo.Marks", "MarkID", c => c.Int(nullable: false));
            AlterColumn("dbo.Routines", "Section", c => c.String());
            AddPrimaryKey("dbo.Marks", new[] { "StudentID", "ProgramCourseID" });
            CreateIndex("dbo.MonthlyFees", "StudentID");
            CreateIndex("dbo.Marks", "ProgramCourseID");
            CreateIndex("dbo.Marks", "StudentID");
            CreateIndex("dbo.StudentEnrollments", "StudentID");
            CreateIndex("dbo.ProgramCourses", "CourseID");
            CreateIndex("dbo.ProgramCourses", "ProgramID");
            AddForeignKey("dbo.StudentFees", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
            AddForeignKey("dbo.MonthlyFees", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
            AddForeignKey("dbo.Marks", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
            AddForeignKey("dbo.StudentEnrollments", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
            AddForeignKey("dbo.FeeStructures", "ProgramID", "dbo.EducationPrograms", "EducationProgramID", cascadeDelete: true);
            AddForeignKey("dbo.Routines", "FacultyID", "dbo.Faculties", "FacultyID", cascadeDelete: true);
            AddForeignKey("dbo.Routines", "ProgramCourseID", "dbo.ProgramCourses", "ProgramCourseID", cascadeDelete: true);
            AddForeignKey("dbo.ProgramCourses", "ProgramID", "dbo.EducationPrograms", "EducationProgramID", cascadeDelete: true);
            AddForeignKey("dbo.Marks", "ProgramCourseID", "dbo.ProgramCourses", "ProgramCourseID", cascadeDelete: true);
            AddForeignKey("dbo.ProgramCourses", "CourseID", "dbo.Courses", "CourseID", cascadeDelete: true);
        }
    }
}
