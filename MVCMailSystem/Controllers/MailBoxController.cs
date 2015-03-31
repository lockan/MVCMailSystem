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
    public class MailBoxController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();

        // GET: /MailBox/
        public ActionResult Index()
        {
            /*
            String joinstring = "SELECT  Employees.username, Mails.text, Mails.dateSent, MailBoxes.dateRcvd, MailBoxes.dateRead FROM MailBoxes"
                + " JOIN Mails ON MailBoxes.mailID = Mails.MailID "
                + " JOIN Employees ON Mails.senderID = Employees.EmployeeID";
            */
            List<MailBox> mailboxlist = null;
            try
            {
                mailboxlist = db.mailboxDB.ToList();
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message); 
            }
            
            return View(mailboxlist);
        }

        // GET: /MailBox/Details/5
        //TODO: Details is broken, needs fixing. Check view first. 
        public ActionResult Details(Guid? id)
        {
            List<MailBox> mailboxlist = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //MailBox mailbox = db.mailboxDB.Find(id);
                mailboxlist = db.mailboxDB.SqlQuery("SELECT * FROM MailBoxes").ToList();
                if (mailboxlist.Count == 0)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message); 
            }
            
            return View(mailboxlist);
        }

        // GET: /MailBox/Delete/5
        //TODO: Delete is broken, needs fixing. Check view first. 
        public ActionResult Delete(Guid? id)
        {
            MailBox mailbox = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mailbox = db.mailboxDB.Find(id);
                if (mailbox == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message); 
            }
            
            return View(mailbox);
        }

        // POST: /MailBox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                MailBox mailbox = db.mailboxDB.Find(id);
                db.mailboxDB.Remove(mailbox);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message); 
            }
            return RedirectToAction("Index"); 
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception ex) 
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message); 
            }
            
        }
    }
}
