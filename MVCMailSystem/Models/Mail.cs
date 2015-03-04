using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    public class Mail
    {
        public Guid                 MailID      { get; set; }   //Primary Key
        public string               text        { get; set; }
        public Nullable<DateTime>   dateSent    { get; set; }
        public Guid                 senderID    { get; set; }   //Foreign Key: userID from employee table
    }
}