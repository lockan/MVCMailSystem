namespace MVCMailSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mailboxID_to_GUID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailBoxes", "MailBoxID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailBoxes", "MailBoxID", c => c.Int(nullable: false, identity: true));
        }
    }
}
