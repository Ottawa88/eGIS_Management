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
    public class NetworkSharesController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: NetworkShares
        public ActionResult Index()
        {
            var networkShare = db.NetworkShare.Include(n => n.GIS_Server);
            return View(networkShare.ToList());
        }

        // GET: NetworkShares/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NetworkShare networkShare = db.NetworkShare.Find(id);
            if (networkShare == null)
            {
                return HttpNotFound();
            }
            return View(networkShare);
        }

        // GET: NetworkShares/Create
        public ActionResult Create()
        {
            ViewBag.ServerID = new SelectList(db.GIS_Server, "Server_ID", "Name");
            return View();
        }

        // POST: NetworkShares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ShareName,Network_Path,ServerID,Description")] NetworkShare networkShare)
        {
            if (ModelState.IsValid)
            {
                db.NetworkShare.Add(networkShare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServerID = new SelectList(db.GIS_Server, "Server_ID", "Name", networkShare.ServerID);
            return View(networkShare);
        }

        // GET: NetworkShares/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NetworkShare networkShare = db.NetworkShare.Find(id);
            if (networkShare == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServerID = new SelectList(db.GIS_Server, "Server_ID", "Name", networkShare.ServerID);
            return View(networkShare);
        }

        // POST: NetworkShares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ShareName,Network_Path,ServerID,Description")] NetworkShare networkShare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(networkShare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServerID = new SelectList(db.GIS_Server, "Server_ID", "Name", networkShare.ServerID);
            return View(networkShare);
        }

        // GET: NetworkShares/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NetworkShare networkShare = db.NetworkShare.Find(id);
            if (networkShare == null)
            {
                return HttpNotFound();
            }
            return View(networkShare);
        }

        // POST: NetworkShares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            NetworkShare networkShare = db.NetworkShare.Find(id);
            db.NetworkShare.Remove(networkShare);
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
