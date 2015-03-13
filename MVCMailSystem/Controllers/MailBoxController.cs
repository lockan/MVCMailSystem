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
            String joinstring = "SELECT  Employees.username, Mails.text, Mails.dateSent, MailBoxes.dateRcvd, MailBoxes.dateRead FROM MailBoxes"
                + " JOIN Mails ON MailBoxes.mailID = Mails.MailID "
                + " JOIN Employees ON Mails.senderID = Employees.EmployeeID";

            //TODO: Fix the duplicate data issue. Need to add an id column to the mailboxes table and use it as the primary key, 
            // but keep the foreign composite keys. Then remap all the values in the view. Should resole the issue. 
            List<MailBox> mailboxlist = db.mailboxDB.ToList();
            
            return View(mailboxlist);
        }

        // GET: /MailBox/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //MailBox mailbox = db.mailboxDB.Find(id);
            List<MailBox> mailboxlist = db.mailboxDB.SqlQuery("SELECT * FROM MailBoxes").ToList();
            if (mailboxlist.Count == 0)
            {
                return HttpNotFound();
            }
            return View(mailboxlist);
        }

        // GET: /MailBox/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailBox mailbox = db.mailboxDB.Find(id);
            if (mailbox == null)
            {
                return HttpNotFound();
            }
            return View(mailbox);
        }

        // POST: /MailBox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MailBox mailbox = db.mailboxDB.Find(id);
            db.mailboxDB.Remove(mailbox);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
