using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCMailSystem.Models;
using System;

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
            try
            {
                return View(db.empDB.ToList());
            } catch (Exception e)
            {
                ViewBag.ErrorMassage = "Unable to retrieve data.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string employees)
        {
            ViewBag.ErrorMassage = "";

            try
            {
                TempData["recipients"] = employees;

                return RedirectToAction("Create", "Mail", new { sender_id = this.Session["RecipientID"] });
            } catch (Exception e)
            {
                ViewBag.ErrorMassage = "Unable to redirect page.";
                return View();
            }
        }
    }
}
