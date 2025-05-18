namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class programCourseFixed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProgramCourses", "Section");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProgramCourses", "Section", c => c.String());
        }
    }
}
