using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    public class Mail
    {
        public Guid                 MailID      { get; set; }   //Primary Key
        public string               MailText        { get; set; }
        public Nullable<DateTime>   DateSent    { get; set; }
        public Guid                 SenderID    { get; set; }   //Foreign Key: userID from employee table
    }
}