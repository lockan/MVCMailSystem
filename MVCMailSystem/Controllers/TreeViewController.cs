using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCMailSystem.Models;
using System.Collections.Generic;
using System.Data;
using MVCMailSystem.ViewModel;

namespace MVCMailSystem.Controllers
{
    public class TreeViewController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();
        //private EmployeeContext db = new EmployeeContext();

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employeelist = db.empDB.ToList();
            List<MailBox> mailboxlist = db.mailboxDB.ToList();
            var viewModel = new TreeViewVM();
            {
                viewModel.EmployeeVM = employeelist;
                viewModel.MailBoxVM = mailboxlist;
            };

            return View(viewModel);
            //return View(db.Employees.ToList());
           // return View(db.empDB.ToList());
        }

        [HttpPost]
        public ActionResult Index(string employees)
        {
            TempData["recipients"] = employees;

            return RedirectToAction("Create", "Mail", new { sender_id = this.Session["RecipientID"] });
        }
    }
}
