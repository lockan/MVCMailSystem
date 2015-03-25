using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Iteration1.DBContexts;
using Iteration1.Models;

namespace Iteration1.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        [HttpPost]
        public ActionResult Index(string employees)
        {
            ViewBag.SelectedEmployees = employees;

            return View(db.Employees.ToList());
        }
    }
}
