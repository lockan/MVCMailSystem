using System;
using System.Data.Entity;

namespace MVCMailSystem.Models
{
    public class Employee
    {
        public Guid                 ID  { get; set; }   //Primary Key
        public string               EmailAddress    { get; set; }
        public string               StaffType   { get; set; }
        public string               FirstName   { get; set; }
        public string               LastName    { get; set; }
        
        //Self referential key to a userID representing the staff member's superior. Nullable.
        public Nullable<Guid>       ManagerID       { get; set; }   
    }
}