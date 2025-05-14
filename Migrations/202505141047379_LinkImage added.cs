namespace KNC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkImageadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "LinkImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "LinkImage");
        }
    }
}
