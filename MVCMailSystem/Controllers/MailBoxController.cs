using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCMailSystem.Models;
using MVCMailSystem.ViewModel;

namespace MVCMailSystem.Controllers
{
    public class MailBoxController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();

        // GET: /MailBox/
        public ActionResult Index()
        {
            /*
            String joinstring = "SELECT  Employees.EmailAddress, Mails.MailText, Mails.DateSent, MailBoxes.DateReceived, MailBoxes.DateRead FROM MailBoxes"
                + " JOIN Mails ON MailBoxes.MailID = Mails.MailID "
                + " JOIN Employees ON Mails.SenderID = Employees.ID";
            */
            IEnumerable<InBox> Mailboxlist = null;
            List<Mail> Maillist = null;
            List<Employee> Employeelist = null;
            

           

            var viewModel = new MailBoxVM();
            try
            {
                //Maillist = db.mailDB.ToList();
                
                string myid = this.Session["UserID"].ToString();
                Guid myguid = new Guid(myid);
                //Mailboxlist = db.mailboxDB.Where(mb => mb.RecipientID == myguid).ToList(); // mail received
                //Maillist = db.mailDB.ToList(); // to get the senderID
               // Employeelist = db.empDB.ToList(); // use senderID to get firstname
                string sqlstatement = "SELECT Mails.*, MailBoxes.*, Employees.Name FROM MailBoxes, Mails, Employees WHERE MailBoxes.RecipientID = '" +
                    myguid + "' AND MailBoxes.MailID = Mails.MailID AND (Mails.senderID = Employees.ID AND MailBoxes.MailID = Mails.MailID)";
                Mailboxlist = db.Database.SqlQuery<InBox>(sqlstatement).ToList();
                
                //get the mails that the user sent
                //Maillist = db.mailDB.Where(mb => mb.SenderID == myguid).ToList(); 

                //viewModel.MailVM = Maillist;
                //viewModel.BoxVM = Mailboxlist;
                //viewModel.EmployeeVM = Employeelist;
                

            
                // mailboxlist = db.mailboxDB.ToList();
                //mailboxlist = db.mailboxDB.Where(mb => mb.RecipientID == myguid).ToList();
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }

            return View(Mailboxlist);
        }

        // GET: /MailBox/Details/5
        //TODO: Details is broken, needs fixing. Check view first. 
        public ActionResult Details(Guid? id, Guid? mail)
        {
            //List<MailBox> mailboxlist = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MailBox mailbox = db.mailboxDB.Find(id);
                Mail mailmsg = db.mailDB.Find(mail);
                // mailboxlist = db.mailboxDB.SqlQuery("SELECT * FROM MailBoxes").ToList();
                if (mailbox == null)
                {
                    return HttpNotFound();
                }
                ViewBag.mailtext = mailmsg.MailText.ToString();
                mailbox.DateRead = DateTime.Now;
                db.SaveChanges();
                return View(mailbox);
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }

            return View();
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

        //For Mobile Demo
        public ActionResult MobileTest()
        {
            string myid = this.Session["UserID"].ToString();
            Guid myguid = new Guid(myid);
            string sqlstatement = "SELECT * FROM Mails";
            IEnumerable<InBox> testlist = db.Database.SqlQuery<InBox>(sqlstatement).ToList();
             
            return View(testlist);
        }
    }
}
