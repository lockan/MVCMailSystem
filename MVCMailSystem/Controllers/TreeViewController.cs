using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCMailSystem.Models;

namespace MVCMailSystem.Controllers
{
    public class TreeViewController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();
        //private EmployeeContext db = new EmployeeContext();

        // GET: Employee
        public ActionResult Index()
        {
            //return View(db.Employees.ToList());
            return View(db.empDB.ToList());
        }

        [HttpPost]
        public ActionResult Index(string employees)
        {
            ViewBag.SelectedEmployees = employees;

            return View(db.empDB.ToList());
        }
    }
}
