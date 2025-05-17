namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsadded : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MonthlyFees", new[] { "StudentId" });
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
            
            AddColumn("dbo.StudentEnrollments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.StudentEnrollments", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.StudentEnrollments", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StudentFees", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.StudentFees", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.StudentFees", "CreatedDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.MonthlyFees", "StudentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MonthlyFees", new[] { "StudentID" });
            DropColumn("dbo.StudentFees", "CreatedDate");
            DropColumn("dbo.StudentFees", "CreatedBy");
            DropColumn("dbo.StudentFees", "IsDeleted");
            DropColumn("dbo.StudentEnrollments", "CreatedDate");
            DropColumn("dbo.StudentEnrollments", "CreatedBy");
            DropColumn("dbo.StudentEnrollments", "IsDeleted");
            DropTable("dbo.Designations");
            CreateIndex("dbo.MonthlyFees", "StudentId");
        }
    }
}
