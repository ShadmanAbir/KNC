namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsprogramfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EducationPrograms", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EducationPrograms", "Duration", c => c.String());
        }
    }
}
