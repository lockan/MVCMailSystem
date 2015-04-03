using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    /* MAILSYSTEMDB.cs
     * This class represents the database context for the mail system. 
     * There is only a single DbContext, and thus only one connection string in the webconfig and a single .mdf file. 
     * This is because we only need a single database with multiple data tables. 
     * Each table has it's own dbset so we can query the tables individually. 
     */
    public class MailSystemDBContext : DbContext
    {
        public DbSet<Employee>  empDB       { get; set; }   // employee table - see Employee.cs
        public DbSet<Mail>      mailDB      { get; set; }   // mail table - see Mail.cs
        public DbSet<MailBox>   mailboxDB   { get; set; }   // mailbox table. - see MailBox.cs  

        
    }
}