namespace MVCMailSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_mailboxes : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.MailBoxes", "empID", c => c.Guid(nullable: false));
            AlterColumn("dbo.MailBoxes", "MailBoxID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailBoxes", "MailBoxID", c => c.Guid(nullable: false));
            DropColumn("dbo.MailBoxes", "empID");
        }
    }
}
