using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STracker;

namespace STracker.Controllers
{
    public class SocalSitesController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: SocalSites
        public ActionResult Index()
        {
            var socalSites = db.SocalSites.Include(s => s.Person);
            return View(socalSites.ToList());
        }

        // GET: SocalSites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocalSite socalSite = db.SocalSites.Find(id);
            if (socalSite == null)
            {
                return HttpNotFound();
            }
            return View(socalSite);
        }

        // GET: SocalSites/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name");
            return View();
        }

        // POST: SocalSites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SiteName,Link,PersonID,Deleted")] SocalSite socalSite)
        {
            if (ModelState.IsValid)
            {
                db.SocalSites.Add(socalSite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", socalSite.PersonID);
            return View(socalSite);
        }

        // GET: SocalSites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocalSite socalSite = db.SocalSites.Find(id);
            if (socalSite == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", socalSite.PersonID);
            return View(socalSite);
        }

        // POST: SocalSites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SiteName,Link,PersonID,Deleted")] SocalSite socalSite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socalSite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", socalSite.PersonID);
            return View(socalSite);
        }

        // GET: SocalSites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocalSite socalSite = db.SocalSites.Find(id);
            if (socalSite == null)
            {
                return HttpNotFound();
            }
            return View(socalSite);
        }

        // POST: SocalSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocalSite socalSite = db.SocalSites.Find(id);
            db.SocalSites.Remove(socalSite);
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
