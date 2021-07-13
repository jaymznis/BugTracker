namespace BugTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userroles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "Complete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ticket", "CompletedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "CompletedBy");
            DropColumn("dbo.Ticket", "Complete");
        }
    }
}
