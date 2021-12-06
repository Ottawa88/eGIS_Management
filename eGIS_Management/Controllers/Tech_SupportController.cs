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
    public class Tech_SupportController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Tech_Support
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Index()
        {
            return View(db.Tech_Support.ToList());
        }

        // GET: Tech_Support/Details/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tech_Support tech_Support = db.Tech_Support.Find(id);
            if (tech_Support == null)
            {
                return HttpNotFound();
            }
            return View(tech_Support);
        }

        // GET: Tech_Support/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tech_Support/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create([Bind(Include = "ID,First_Name,Last_Name,Email,Phone")] Tech_Support tech_Support)
        {
            if (ModelState.IsValid)
            {
                db.Tech_Support.Add(tech_Support);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tech_Support);
        }

        // GET: Tech_Support/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tech_Support tech_Support = db.Tech_Support.Find(id);
            if (tech_Support == null)
            {
                return HttpNotFound();
            }
            return View(tech_Support);
        }

        // POST: Tech_Support/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit([Bind(Include = "ID,First_Name,Last_Name,Email,Phone")] Tech_Support tech_Support)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tech_Support).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tech_Support);
        }

        // GET: Tech_Support/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tech_Support tech_Support = db.Tech_Support.Find(id);
            if (tech_Support == null)
            {
                return HttpNotFound();
            }
            return View(tech_Support);
        }

        // POST: Tech_Support/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tech_Support tech_Support = db.Tech_Support.Find(id);
            db.Tech_Support.Remove(tech_Support);
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
