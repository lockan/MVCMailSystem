using System;
using System.Data.Entity;
using MVCMailSystem.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace MVCMailSystem.Models
{
    // Mailbox does not store message MailText, just references to appropriate IDs to find the message. 
    // Better for space conservation, but requires table join on Mail.MailID to get full message details. 
    public class InBox
    {
        
        //Surrogate Primary key: id - because E.F. hates composite keys. 
        public Guid                 MailBoxID    { get; set; }
        
        //Foreign key 1: mailboxID is the recipient's employeeID from employee table. 
        public Guid                 RecipientID       { get; set; }   
        
        //Foreign key 2: messageID is the messageID from the Message table.  
        public Guid                 MailID      { get; set; }      

        //Timestamp when user received the message - nullable.
        //"New" messages have a NULL value, so display those and highlight them. 
        public Nullable<DateTime>   DateReceived    { get; set; }
        
        //Timestamp when user actually reads the message (e.g. onClick or onSelected event?)
        //Set this when the user reads the message. NULL = "unread".  
        public Nullable<DateTime>   DateRead    { get; set; }
  
   
        public string MailText { get; set; }
  
        public Nullable<DateTime> DateSent { get; set; }

        public Guid SenderID { get; set; }   //Foreign Key: userID from employee table

        public Guid ID { get; set; }   //Primary Key
        public string Name { get; set; }


        //Self referential key to a userID representing the staff member's superior. Nullable.
        public Nullable<Guid> ManagerID { get; set; }   
        [NotMapped]
        public IEnumerable<MailBox> maillist;
    }
}