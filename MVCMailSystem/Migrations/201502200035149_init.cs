namespace MVCMailSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Guid(nullable: false),
                        username = c.String(),
                        stafftype = c.String(),
                        firstname = c.String(),
                        lastname = c.String(),
                        mgrID = c.Guid(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.MailBoxes",
                c => new
                    {
                        MailBoxID = c.Guid(nullable: false),
                        mailID = c.Guid(nullable: false),
                        dateRcvd = c.DateTime(),
                        dateRead = c.DateTime(),
                    })
                .PrimaryKey(t => t.MailBoxID);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        MailID = c.Guid(nullable: false),
                        text = c.String(),
                        dateSent = c.DateTime(),
                        senderID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MailID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mails");
            DropTable("dbo.MailBoxes");
            DropTable("dbo.Employees");
        }
    }
}
