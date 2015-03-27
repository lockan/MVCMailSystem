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
            return View(db.mailDB.ToList());
        }

        // GET: /Mail/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.mailDB.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // GET: /Mail/Create
        public ActionResult Create(Guid sender_id)
        {
            Mail m = new Mail();
            m.senderID = sender_id;
            
            return View(m);
        }

        // POST: /Mail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MailID,text,dateSent,senderID")] Mail mail)
        {
            //Wonho to set list of recipient GUIDS in a TempData[].
            //Need to retrieve that list and generate an Employee List here. 
            //TEMP: Currently sending to all employees.
            List<Employee> recipients = db.empDB.ToList();
            
            //TEST TEST TEST
            string gotem = ViewBag.SelectedEmployees;
                        
            //Set timestamp on message. 
            DateTime timesent = new DateTime(); timesent = DateTime.Now;
            mail.dateSent = timesent;
            
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
                    mailbox.mailID = mail.MailID;   //Only need to set the mailID once. 
                    mailbox.empID = emp.EmployeeID;
                    mailbox.dateRcvd = null;
                    mailbox.dateRead = null;
                    db.mailboxDB.Add(mailbox);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(mail);
        }

//TODO:  DELETE THE Delete Action. Mail should never be deleted from the mail table. 

        // GET: /Mail/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.mailDB.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // POST: /Mail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Mail mail = db.mailDB.Find(id);
            db.mailDB.Remove(mail);
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
