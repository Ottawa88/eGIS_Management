using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eGIS_Management.Models;
using eGIS_Management.Models.ViewModels;

namespace eGIS_Management.Controllers
{
    public class GIS_EnvironmentController : Controller
    {
        private IMTS_GISEntities db = new IMTS_GISEntities();

        // GET: GIS_Environment
        public ActionResult Index(int? id, int? Server_ID)
        {
            var viewModel = new GIS_EnvironmentIndexData();
            viewModel.GIS_Environments = db.GIS_Environment.Include(i => i.GIS_Server)
                                                        .Include(i => i.GIS_Server.Select(c =>c.Software))
                                                        .OrderBy(i => i.Name);
            if (id != null)
            {
                ViewBag.Environment_ID = id.Value;
                viewModel.GIS_Servers= viewModel.GIS_Environments.Where(
                    i => i.Environment_ID == id.Value).Single().GIS_Server.OrderBy(i=>i.Name);

                ViewBag.EnvDiagram = db.GIS_Environment.Find(id).Diagram_Name;
            }
            if (Server_ID != null)
            {
                ViewBag.Server_ID = Server_ID.Value;
                viewModel.Softwares = viewModel.GIS_Servers.Where(
                    x => x.Server_ID == Server_ID).Single().Software.OrderBy(i=>i.Name_Version);
            }
            return View(viewModel);
        }
       


        // GET: GIS_Environment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Environment gIS_Environment = db.GIS_Environment.Find(id);
            if (gIS_Environment == null)
            {
                return HttpNotFound();
            }
            return View(gIS_Environment);
        }

        // GET: GIS_Environment/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GIS_Environment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create(/*[Bind(Include = "Environment_ID,Name,Description,URL,Last_Updated,Last_UpdatedBy")] */GIS_Environment gIS_Environment, HttpPostedFileBase diagram)
        {
            if (ModelState.IsValid)
            {

                if (diagram != null && diagram.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(diagram.FileName);
                    var path = Path.Combine(Server.MapPath("~/Data_Content/"), fileName);

                    diagram.SaveAs(path);
                    gIS_Environment.Diagram_Name= fileName;
                }
                gIS_Environment.Last_Updated = DateTime.UtcNow;
                gIS_Environment.Last_UpdatedBy = User.Identity.Name;
                db.GIS_Environment.Add(gIS_Environment);
               
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            return View(gIS_Environment);
        }

        // GET: GIS_Environment/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Environment gIS_Environment = db.GIS_Environment.Find(id);
            if (gIS_Environment == null)
            {
                return HttpNotFound();
            }
            return View(gIS_Environment);
        }

        // POST: GIS_Environment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int? id, HttpPostedFileBase diagram)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var envToUpdate = db.GIS_Environment.Find(id);
            if (TryUpdateModel(envToUpdate, "",
        new string[] { "Name", "Description", "URL" }))
            {
                try
                {
                    if (diagram != null && diagram.ContentLength > 0)
                    {
                        //Check file extension
                        //Check if there is an existing diagram, if yes, delete it
                        if (envToUpdate.Diagram_Name != null)
                        {

                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Data_Content/"), envToUpdate.Diagram_Name));
                        }
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + Path.GetFileName(diagram.FileName);
                        var path = Path.Combine(Server.MapPath("~/Data_Content/"), fileName);

                        diagram.SaveAs(path);
                        envToUpdate.Diagram_Name = fileName;
                    }
                    envToUpdate.Last_Updated= DateTime.UtcNow;
                    envToUpdate.Last_UpdatedBy= User.Identity.Name;
                    db.Entry(envToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (System.Data.Entity.Infrastructure.RetryLimitExceededException /* dex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(envToUpdate);
        }
       

        public FileResult Download(int id)
        {
            GIS_Environment env = db.GIS_Environment.Find(id);
            string fileName;
            byte[] fileBytes;
            if (env == null)
            {
                fileName = "eGIS Architecture FULL.PNG";
                fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Data_Content/"), fileName));
                
            }
            else
            {
                fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Data_Content/"), env.Diagram_Name));
                fileName = env.Diagram_Name;
            }

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // GET: GIS_Environment/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIS_Environment gIS_Environment = db.GIS_Environment.Find(id);
            if (gIS_Environment == null)
            {
                return HttpNotFound();
            }
            return View(gIS_Environment);
        }

        // POST: GIS_Environment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GIS_Environment gIS_Environment = db.GIS_Environment.Find(id);
            //Check if there is an existing diagram, if yes, delete it
            if (gIS_Environment.Diagram_Name != null)
            {

                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Data_Content/"), gIS_Environment.Diagram_Name));
            }

            db.GIS_Environment.Remove(gIS_Environment);
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
