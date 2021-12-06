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
    public class ApplicationsController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: Applications
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? Page)
            
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.EnvironmentSortParm = sortOrder == "Environment" ? "Environment_desc" : "Environment";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "Type_desc" : "Type";
            ViewBag.APMSortParm = sortOrder == "APM" ? "APM_desc" : "APM";
            ViewBag.ClientContact1SortParm = sortOrder == "ClientContact1" ? "ClientContact1_desc" : "ClientContact1";
            ViewBag.TechSupport1SortParm = sortOrder == "TechSupport1" ? "TechSupport1_desc" : "TechSupport1";
            ViewBag.DFORegionSortParm = sortOrder == "RegionID" ? "DFORegion_desc" : "RegionID";
            if (searchString != null)
            {
                Page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var application = db.Application.Include(a => a.Client_Contact).Include(a => a.Client_Contact1).Include(a => a.GIS_Environment).Include(a => a.Tech_Support).Include(a => a.Tech_Support1).Include(a => a.ApplicationType).Include(a =>a.DFO_Region); 
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
                    application = application.OrderByDescending(s => s.Name);
                    break;
                case "Environment_desc":
                    application = application.OrderByDescending(s => s.GIS_Environment.Name);
                    break;
                case "Type_desc":
                    application = application.OrderByDescending(s => s.ApplicationType.TypeName);
                    break;
                case "DFORegion_desc":
                    application = application.OrderByDescending(s => s.DFO_Region.Region);
                    break;
                case "APM_desc":
                    application = application.OrderByDescending(s => s.APM_ID);
                    break;
                case "ClientContact1_desc":
                    application = application.OrderByDescending(s => s.ClientContact1);
                    break;
                case "TechSupport1_desc":
                    application = application.OrderByDescending(s => s.TechSupport1);
                    break;
                case "Name":
                    application = application.OrderBy(s => s.Name);
                    break;

                case "Environment":
                    application = application.OrderBy(s => s.GIS_Environment.Name);
                    break;
                case "Type":
                    application = application.OrderBy(s => s.ApplicationType.TypeName);
                    break;
                case "RegionID":
                    application = application.OrderBy(s => s.DFO_Region.Region);
                    break;
                case "APM":
                    application = application.OrderBy(s => s.APM_ID);
                    break;
                case "ClientContact1":
                    application = application.OrderBy(s => s.ClientContact1);
                    break;
                case "TechSupport1":
                    application = application.OrderBy(s => s.TechSupport1);
                    break;
                default:

                    application = application.OrderBy(s => s.Name);
                    break;
            }
            ViewBag.TotalCount = application.Count();
            int pageSize = 10;
            int pageNumber = (Page ?? 1);
         
            return View(application.ToPagedList(pageNumber, pageSize));
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
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create()
        {
            ViewBag.ClientContact1 = new SelectList(db.Client_Contact, "Client_ID", "Email");
            ViewBag.ClientContact2 = new SelectList(db.Client_Contact, "Client_ID", "Email");
            ViewBag.GIS_Environment_ID = new SelectList(db.GIS_Environment, "Environment_ID", "Name");
            ViewBag.TechSupport1 = new SelectList(db.Tech_Support, "ID", "Email");
            ViewBag.TechSupport2 = new SelectList(db.Tech_Support, "ID", "Email");
            ViewBag.TypeID = new SelectList(db.ApplicationType, "TypeID", "TypeName");
            ViewBag.RegionID = new SelectList(db.DFO_Region, "Region_ID", "Region");
            ViewBag.SectorID = new SelectList(db.RegionSectors, "SectorID", "SectorName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create([Bind(Include = "App_ID,Name,APM_ID,GIS_Environment_ID,URL_En,URL_Fr,Description,TechSupport1,TechSupport2,ClientContact1,ClientContact2,DevOps_Link_En,Technical_Doc_Link,Note,Last_Updated,Last_UpdatedBy, TypeID, RegionID, SectorID")] Application application)
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
            ViewBag.TypeID = new SelectList(db.ApplicationType, "TypeID", "TypeName", application.TypeID);
            ViewBag.RegionID = new SelectList(db.DFO_Region, "Region_ID", "Region", application.RegionID);
            ViewBag.SectorID = new SelectList(db.RegionSectors, "SectorID", "SectorName", application.SectorID);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin,Editor")]
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
            ViewBag.TypeID = new SelectList(db.ApplicationType, "TypeID", "TypeName", application.TypeID);
            ViewBag.RegionID= new SelectList(db.DFO_Region, "Region_ID", "Region", application.RegionID);
            ViewBag.SectorID= new SelectList(db.RegionSectors, "SectorID", "SectorName", application.SectorID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit([Bind(Include = "App_ID,Name,APM_ID,GIS_Environment_ID,URL_En,URL_Fr,Description,TechSupport1,TechSupport2,ClientContact1,ClientContact2,DevOps_Link_En,Technical_Doc_Link,Note,Last_Updated,Last_UpdatedBy, TypeID, RegionID, SectorID")] Application application)
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
            ViewBag.TypeID = new SelectList(db.ApplicationType, "TypeID", "TypeName", application.TypeID);
            ViewBag.RegionID = new SelectList(db.DFO_Region, "Region_ID", "Region", application.RegionID);
            ViewBag.SectorID = new SelectList(db.RegionSectors, "SectorID", "SectorName", application.SectorID);
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Application.Find(id);
            db.Application.Remove(application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetSectorListByRegionID(int regionID)
        {

            List<RegionSectors> sectors = (from a in db.RegionSectors where a.RegionID == regionID orderby a.SectorName select a).ToList();
            SelectList sectorList = new SelectList(sectors, "SectorID", "SectorName");
            return Json(sectorList, JsonRequestBehavior.AllowGet);

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
