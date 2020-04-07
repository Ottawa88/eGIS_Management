using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eGIS_Management.Models;

namespace eGIS_Management.Controllers
{
    public class ApplicationsController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var application = db.Application.Include(a => a.Client_Contact).Include(a => a.Client_Contact1).Include(a => a.GIS_Environment).Include(a => a.Tech_Support).Include(a => a.Tech_Support1);
            return View(application.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ClientContact1 = new SelectList(db.Client_Contact, "Client_ID", "Email");
            ViewBag.ClientContact2 = new SelectList(db.Client_Contact, "Client_ID", "Email");
            ViewBag.GIS_Environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name");
            ViewBag.TechSupport1 = new SelectList(db.Tech_Support, "ID", "Email");
            ViewBag.TechSupport2 = new SelectList(db.Tech_Support, "ID", "Email");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "App_ID,Name,APM_ID,GIS_Environment_ID,URL_En,URL_Fr,Description,TechSupport1,TechSupport2,ClientContact1,ClientContact2,DevOps_Link_En,DevOps_Link_Fr,Technical_Doc_Link,Note,Last_Updated,Last_UpdatedBy")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Application.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientContact1 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact1);
            ViewBag.ClientContact2 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact2);
            ViewBag.GIS_Environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", application.GIS_Environment_ID);
            ViewBag.TechSupport1 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport1);
            ViewBag.TechSupport2 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport2);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientContact1 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact1);
            ViewBag.ClientContact2 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact2);
            ViewBag.GIS_Environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", application.GIS_Environment_ID);
            ViewBag.TechSupport1 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport1);
            ViewBag.TechSupport2 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport2);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "App_ID,Name,APM_ID,GIS_Environment_ID,URL_En,URL_Fr,Description,TechSupport1,TechSupport2,ClientContact1,ClientContact2,DevOps_Link_En,DevOps_Link_Fr,Technical_Doc_Link,Note,Last_Updated,Last_UpdatedBy")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientContact1 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact1);
            ViewBag.ClientContact2 = new SelectList(db.Client_Contact, "Client_ID", "Email", application.ClientContact2);
            ViewBag.GIS_Environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", application.GIS_Environment_ID);
            ViewBag.TechSupport1 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport1);
            ViewBag.TechSupport2 = new SelectList(db.Tech_Support, "ID", "Email", application.TechSupport2);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Application.Find(id);
            db.Application.Remove(application);
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
