namespace BugTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.Guid(nullable: false),
                        Attachedby = c.String(),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        TicketId = c.Int(nullable: false),
                        URL = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "TicketId", "dbo.Ticket");
            DropIndex("dbo.Attachment", new[] { "TicketId" });
            DropTable("dbo.Attachment");
        }
    }
}
