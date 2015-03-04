using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    public class Employee
    {
        public Guid                 EmployeeID  { get; set; }   //Primary Key
        public string               username    { get; set; }
        public string               stafftype   { get; set; }
        public string               firstname   { get; set; }
        public string               lastname    { get; set; }
        
        //Self referential key to a userID representing the staff member's superior. Nullable.
        public Nullable<Guid>       mgrID       { get; set; }   
    }
}