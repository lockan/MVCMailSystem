using System;
using System.Collections.Generic;

using System.Data.Entity;

using MVCMailSystem.Models;

namespace MVCMailSystem.ViewModel
{
    public class TreeViewVM
    {
        public List<MailBox> MailBoxVM { get; set; }
        public List<Employee> EmployeeVM { get; set; }
    }
}
