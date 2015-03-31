using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCMailSystem.Models;

namespace MVCMailSystem.Controllers
{
    public class LoginController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();

        // GET: /Login/
        public ActionResult Index()
        {
            if (TempData["errorMessage"] != null) @ViewBag.errorMessage = TempData["errorMessage"].ToString();
            //SeleniumDriver selenium = new SeleniumDriver();
            //selenium.LoginTest();
            /*
            List<SelectListItem> arbitrarylist = new List<SelectListItem>
            {
                new SelectListItem { Text = "Arbitrary 1", Value="Arbitrary1" },
                new SelectListItem { Text = "Arbitrary 2", Value="Arbitrary2" },
                new SelectListItem { Text = "Arbitrary 3", Value="Arbitrary3" }
            };
            ViewBag.alist = arbitrarylist;
            */
            return View();
        }

        public ActionResult Login(string username, string ArbitraryList)
        {

            try
            {
                Employee emp = db.empDB.SingleOrDefault(user => user.username == username);
                if (emp != null)
                //if (db.empDB.Find(username) != null)
                {
                    this.Session.Add("username", emp.username);
                    this.Session.Add("stafftype", emp.stafftype);
                    this.Session.Add("empID", emp.EmployeeID);
                    this.Session.Add("mgrID", emp.mgrID);

                    switch (emp.stafftype)
                    {
                        case ("admin"):
                            {
                                return RedirectToAction("Index", "Mail", new { username = username });
                            }
                        case ("manager"):
                            {
                                return RedirectToAction("Index", "Mail", new { username = username });
                            }
                        case ("staff"):
                            {
                                return RedirectToAction("Index", "Mail", new { username = username });
                            }
                        default:
                            {
                                TempData["errorMessage"] = "ERROR: Could not identify user staff type. Please try again.";
                                return RedirectToAction("Index");
                            }
                    }
                }
                else
                {
                    TempData["errorMessage"] = "ERROR: Bad username or password. Please try again.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
