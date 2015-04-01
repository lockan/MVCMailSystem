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
    public class MailController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();

        // GET: /Mail/
        public ActionResult Index()
        {
            List<Mail> empList = null;
            try
            {
                empList = db.mailDB.ToList();
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(empList);
        }

        // GET: /Mail/Details/5
        public ActionResult Details(Guid? id)
        {
            Mail mail = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mail = db.mailDB.Find(id);
                if (mail == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            return View(mail);
        }

        // GET: /Mail/Create
        public ActionResult Create(Guid sender_id)
        {
            Mail m = null;
            try
            {
                m = new Mail();
                m.SenderID = sender_id;
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(m);
        }

        // POST: /Mail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MailID,MailText,DateSent,SenderID")] Mail mail)
        {
            List<Employee> recipients = null;
            
            try 
            {
                //Retrieve that list and generate an Employee List here.     
                string ids = TempData["recipients"].ToString();
                string[] ids_parsed = ids.Split(',');
                string ids_formatted = "";
                foreach (string idstr in ids_parsed)
                {
                    ids_formatted += "'" + idstr + "', ";
                }
                ids_formatted = ids_formatted.Substring(0, ids_formatted.Length - 2);

                recipients =
                    db.empDB.SqlQuery("SELECT * FROM employees "
                        + "WHERE ID IN ( " + ids_formatted + " )").ToList();

                //Set timestamp on message. 
                DateTime timesent = new DateTime(); timesent = DateTime.Now;
                mail.DateSent = timesent;

                if (ModelState.IsValid)
                {
                    //populate new message in Mail table. 
                    mail.MailID = Guid.NewGuid();
                    db.mailDB.Add(mail);
                    db.SaveChanges();

                    //END TEMP

                    //add message links to MailBox table for all users in list. 

                    foreach (Employee emp in recipients)
                    {
                        MailBox mailbox = new MailBox();
                        mailbox.MailBoxID = Guid.NewGuid();
                        mailbox.MailID = mail.MailID;   //Only need to set the MailID once. 
                        mailbox.RecipientID = emp.ID;
                        mailbox.DateReceived = null;
                        mailbox.DateRead = null;
                        db.mailboxDB.Add(mailbox);
                    }
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }

            return View(mail);
        }

//TODO:  DELETE THE Delete Action. Mail should never be deleted from the mail table. 

        // GET: /Mail/Delete/5
        public ActionResult Delete(Guid? id)
        {
            Mail mail = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mail = db.mailDB.Find(id);
                if (mail == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(mail);
        }

        // POST: /Mail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                Mail mail = db.mailDB.Find(id);
                db.mailDB.Remove(mail);
                db.SaveChanges();
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
