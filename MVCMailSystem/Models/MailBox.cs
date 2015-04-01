using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    // Mailbox does not store message MailText, just references to appropriate IDs to find the message. 
    // Better for space conservation, but requires table join on Mail.MailID to get full message details. 
    public class MailBox
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
        public Nullable<DateTime>   DateRead    { get; set; }   //

    }
}