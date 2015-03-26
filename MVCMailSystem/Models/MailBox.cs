using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    // Mailbox does not store message text, just references to appropriate IDs to find the message. 
    // Better for space conservation, but requires table join on Mail.mailID to get full message details. 
    public class MailBox
    {
        
        //Surrogate Primary key: id - because E.F. hates composite keys. 
        public Guid                 MailBoxID    { get; set; }
        
        //Foreign key 1: mailboxID is the recipient's employeeID from employee table. 
        public Guid                 empID       { get; set; }   
        
        //Foreign key 2: messageID is the messageID from the Message table.  
        public Guid                 mailID      { get; set; }      

        //Timestamp when user received the message - nullable.
        //"New" messages have a NULL value, so display those and highlight them. 
        public Nullable<DateTime>   dateRcvd    { get; set; }
        
        //Timestamp when user actually reads the message (e.g. onClick or onSelected event?)
        //Set this when the user reads the message. NULL = "unread".  
        public Nullable<DateTime>   dateRead    { get; set; }   //

    }
}