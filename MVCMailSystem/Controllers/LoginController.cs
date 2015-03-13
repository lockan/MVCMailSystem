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
            return View();
        }

        public ActionResult Login(string username)
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
                            return RedirectToAction("Index", "Home", new { username = username });
                        }
                    case ("manager"):
                        {
                            return RedirectToAction("Index", "Home", new { username = username });
                        }
                    case ("staff"):
                        {
                            return RedirectToAction("Index", "Home", new { username = username });
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
    }
}
