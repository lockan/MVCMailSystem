using System;
using System.Collections.Generic;

using System.Data.Entity;

using MVCMailSystem.Models;

namespace MVCMailSystem.ViewModel
{

    public class MailBoxVM
    {
        public List<Employee> EmployeeVM { get; set; }
        public List<Mail> MailVM { get; set; }
        public List<MailBox> BoxVM { get; set; }

        public string To { get; set; } // uses firstname from Employee
        public string From { get; set; } // uses firstname from Employee
        public string MailText { get; set; } // the mail message from Mails
        public DateTime DateRead { get; set; }  //from Mailboxes
        public DateTime DateSent { get; set; } // from Mails

    }
  
}