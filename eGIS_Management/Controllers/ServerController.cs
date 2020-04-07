using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using eGIS_Management.Models;

namespace eGIS_Management.Controllers
{
    public class ServerController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Server
       
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? Page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.EnvironmentSortParm = sortOrder == "Environment" ? "Environment_desc" : "Environment";
            ViewBag.ZoneSortParm = sortOrder == "Zone" ? "Zone_desc" : "Zone";
            ViewBag.RegionSortParm = sortOrder == "Region" ? "Region_desc" : "Region";
            ViewBag.Diskspace_GBSortParm = sortOrder == "Diskspace_GB" ? "Diskspace_GB_desc" : "Diskspace_GB";
            ViewBag.RAM_GBSortParm = sortOrder == "RAM_GBCompleted" ? "RAM_GB_desc" : "RAM_GB";
            ViewBag.CPUSortParm = sortOrder == "CPU" ? "CPU_desc" : "CPU";
            ViewBag.IP_AddressSortParm = sortOrder == "IP_Address" ? "IP_Address_desc" : "IP_Address";


            if (searchString != null)
            {
                Page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //Eager loading
            var dataItems = db.GIS_Server.Include(d => d.DFO_Region).Include(d => d.GIS_Environment).Include(d => d.Network_Zone);

            if (!String.IsNullOrEmpty(searchString))
            {
                //searchString = searchString.Trim();
                //dataItems = dataItems.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                //                     || s.DataOwner.ToUpper().Contains(searchString.ToUpper())
                //                     || s.URL.ToUpper().Contains(searchString.ToUpper())
                //                     || s.DataOwnerEmail.ToUpper().Contains(searchString.ToUpper())

                //                     );
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    dataItems = dataItems.OrderByDescending(s => s.Name);
                    break;
                case "Environment_desc":
                    dataItems = dataItems.OrderByDescending(s => s.GIS_Environment.Name);
                    break;
                case "Zone_desc":
                    dataItems = dataItems.OrderByDescending(s => s.Network_Zone.Name);
                    break;
                case "Region_desc":
                    dataItems = dataItems.OrderByDescending(s => s.DFO_Region.Region);
                    break;
                case "Diskspace_GB_desc":
                    dataItems = dataItems.OrderByDescending(s => s.Diskspace_GB);
                    break;
                case "RAM_GB_desc":
                    dataItems = dataItems.OrderByDescending(s => s.RAM_GB);
                    break;
                case "CPU_desc":
                    dataItems = dataItems.OrderByDescending(s => s.CPU);
                    break;
                case "IP_Address_desc":
                    dataItems = dataItems.OrderByDescending(s => s.IP_Address);
                    break;
                case "Name":
                    dataItems = dataItems.OrderBy(s => s.Name);
                    break;
                case "Environment":
                    dataItems = dataItems.OrderBy(s => s.GIS_Environment.Name);
                    break;
                case "Zone":
                    dataItems = dataItems.OrderBy(s => s.Network_Zone.Name);
                    break;
                case "Region":
                    dataItems = dataItems.OrderBy(s => s.DFO_Region.Region);
                    break;
                case "Diskspace_GB":
                    dataItems = dataItems.OrderBy(s => s.Diskspace_GB);
                    break;
                case "RAM_GB":
                    dataItems = dataItems.OrderBy(s => s.RAM_GB);
                    break;
                case "CPU":
                    dataItems = dataItems.OrderBy(s => s.CPU);
                    break;
                case "IP_Address":
                    dataItems = dataItems.OrderBy(s => s.IP_Address);
                    break;
                default:

                    dataItems = dataItems.OrderByDescending(s => s.Name);
                    break;

            }
            ViewBag.TotalCount = dataItems.Count();
            int pageSize = 10;
            int pageNumber = (Page ?? 1);
            return View(dataItems.ToPagedList(pageNumber, pageSize));
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
            ViewBag.OS_ID = new SelectList(db.OS, "OS_ID", "OS_Name");
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name");
            ViewBag.Region_ID = new SelectList(db.DFO_Region, "Region_ID", "Region");
            return View();
        }

        // POST: Server/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Server_ID,Name,FQDN,Zone_ID, Port_in_Use,IP_Address,Region_ID,gis_environment_ID,OS_ID,Diskspace_GB,RAM_GB,CPU,SSL_IssuedTo,SSL_IssuedBy,SSL_ExpiryDate,Note,Last_Updated,Last_Updated_By")] GIS_Server gIS_Server)
        {
            if (ModelState.IsValid)
            {
                db.GIS_Server.Add(gIS_Server);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", gIS_Server.gis_environment_ID);
            ViewBag.OS_ID = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.Region_ID= new SelectList(db.DFO_Region, "Region_ID", "Region", gIS_Server.Region_ID);
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
            ViewBag.OS_ID = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.Region_ID = new SelectList(db.DFO_Region, "Region_ID", "Region", gIS_Server.Region_ID);
            return View(gIS_Server);
        }

        // POST: Server/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Server_ID,Name,FQDN,Zone_ID,Port_in_Use,IP_Address,Region_ID,gis_environment_ID,OS_ID,Diskspace_GB,RAM_GB,CPU,SSL_IssuedTo,SSL_IssuedBy,SSL_ExpiryDate,Note,Last_Updated,Last_Updated_By")] GIS_Server gIS_Server)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIS_Server).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gis_environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name", gIS_Server.gis_environment_ID);
            ViewBag.OS_ID = new SelectList(db.OS, "OS_ID", "OS_Name", gIS_Server.OS_ID);
            ViewBag.Zone_ID = new SelectList(db.Network_Zone, "Zone_ID", "Name", gIS_Server.Zone_ID);
            ViewBag.Region_ID = new SelectList(db.DFO_Region, "Region_ID", "Region", gIS_Server.Region_ID);
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
