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
    public class ServerController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Server
        public ActionResult Index()
        {
            var gIS_Server = db.GIS_Server.Include(g => g.GIS_Environment).Include(g => g.Network_Zone).Include(g => g.OS1);
            return View(gIS_Server.ToList());
        }

        // GET: Server/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Server gIS_Server = db.GIS_Server.Find(id);
            if (gIS_Server == null)
            {
                return HttpNotFound();
            }
            return View(gIS_Server);
        }

        // GET: Server/Create
        public ActionResult Create()
        {
            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name");
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name");
            ViewBag.OS = new SelectList(db.OS, "OS_ID", "OS_Name");
            return View();
        }

        // POST: Server/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Server_ID,Name,FQDN,Zone_ID,IP_Address,gis_environment_ID,OS,Diskspace_GB,RAM_GB,CPU,Note,Last_Updated,Last_Updated_By")] GIS_Server gIS_Server)
        {
            if (ModelState.IsValid)
            {
                db.GIS_Server.Add(gIS_Server);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", gIS_Server.gis_environment_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.OS = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS);
            return View(gIS_Server);
        }

        // GET: Server/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Server gIS_Server = db.GIS_Server.Find(id);
            if (gIS_Server == null)
            {
                return HttpNotFound();
            }
            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", gIS_Server.gis_environment_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.OS = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS);
            return View(gIS_Server);
        }

        // POST: Server/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Server_ID,Name,FQDN,Zone_ID,IP_Address,gis_environment_ID,OS,Diskspace_GB,RAM_GB,CPU,Note,Last_Updated,Last_Updated_By")] GIS_Server gIS_Server)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIS_Server).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", gIS_Server.gis_environment_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.OS = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS);
            return View(gIS_Server);
        }

        // GET: Server/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Server gIS_Server = db.GIS_Server.Find(id);
            if (gIS_Server == null)
            {
                return HttpNotFound();
            }
            return View(gIS_Server);
        }

        // POST: Server/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GIS_Server gIS_Server = db.GIS_Server.Find(id);
            db.GIS_Server.Remove(gIS_Server);
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
