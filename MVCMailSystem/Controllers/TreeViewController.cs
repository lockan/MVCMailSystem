using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCMailSystem.Models;
using System;
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
            ViewBag.ErrorMassage = "";
            List<Employee> employeelist = null;
            List<MailBox> mailboxlist = null;

            try
            {
                employeelist = db.empDB.ToList();
                mailboxlist = db.mailboxDB.ToList();

                var viewModel = new TreeViewVM();
                {
                    viewModel.EmployeeVM = employeelist;
                    viewModel.MailBoxVM = mailboxlist;
                };
                return View(viewModel);

                //return View(db.empDB.ToList());
            } 
            catch (Exception e)
            {
                ViewBag.ErrorMassage = "Unable to retrieve data.";
                return View();
            }
            
            //return View(db.Employees.ToList());
           // return View(db.empDB.ToList());
        }

        [HttpPost]
        public ActionResult Index(string employees)
        {
            ViewBag.ErrorMassage = "";

            try
            {
                TempData["recipients"] = employees;

                return RedirectToAction("Create", "Mail", new { sender_id = this.Session["UserID"] });
            } catch (Exception e)
            {
                ViewBag.ErrorMassage = "Unable to redirect page.";
                return View();
            }
        }
    }
}
