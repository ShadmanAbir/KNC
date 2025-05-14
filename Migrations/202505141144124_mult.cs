namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationPrograms",
                c => new
                    {
                        EducationProgramID = c.Int(nullable: false, identity: true),
                        ProgramName = c.String(),
                        ShortName = c.String(),
                        Duration = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EducationProgramID);
            
            DropTable("dbo.DropDowns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DropDowns",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.EducationPrograms");
        }
    }
}
