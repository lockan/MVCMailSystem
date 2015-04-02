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
    public class EmployeeController : Controller
    {
        private MailSystemDBContext db = new MailSystemDBContext();

        // GET: /Employee/
        public ActionResult Index()
        {
            List<Employee> empList = null;
            try
            {
                empList = db.empDB.ToList();
                
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            return View(empList);
        }

        // GET: /Employee/Details/5
        public ActionResult Details(Guid? id)
        {
            Employee employee = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                employee = db.empDB.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,EmailAddress,StaffType,FirstName,Name,ManagerID")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.ID = Guid.NewGuid();
                    db.empDB.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }

            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(Guid? id)
        {
            Employee employee = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                employee = db.empDB.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EmailAddress,StaffType,FirstName,Name,ManagerID")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
            
            return View(employee);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(Guid? id)
        {
            Employee employee = null;
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                employee = db.empDB.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: Use Terrence's logging function. 
                System.Diagnostics.EventLog.WriteEntry("MVCMailSystem", ex.Message);
            }
           
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                Employee employee = db.empDB.Find(id);
                db.empDB.Remove(employee);
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
