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
    public class Client_ContactController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Client_Contact
        public ActionResult Index()
        {
            return View(db.Client_Contact.ToList());
        }

        // GET: Client_Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_Contact client_Contact = db.Client_Contact.Find(id);
            if (client_Contact == null)
            {
                return HttpNotFound();
            }
            return View(client_Contact);
        }

        // GET: Client_Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client_Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Client_ID,First_Name,Last_Name,Email,Phone")] Client_Contact client_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Client_Contact.Add(client_Contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client_Contact);
        }

        // GET: Client_Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_Contact client_Contact = db.Client_Contact.Find(id);
            if (client_Contact == null)
            {
                return HttpNotFound();
            }
            return View(client_Contact);
        }

        // POST: Client_Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Client_ID,First_Name,Last_Name,Email,Phone")] Client_Contact client_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client_Contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client_Contact);
        }

        // GET: Client_Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_Contact client_Contact = db.Client_Contact.Find(id);
            if (client_Contact == null)
            {
                return HttpNotFound();
            }
            return View(client_Contact);
        }

        // POST: Client_Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client_Contact client_Contact = db.Client_Contact.Find(id);
            db.Client_Contact.Remove(client_Contact);
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
