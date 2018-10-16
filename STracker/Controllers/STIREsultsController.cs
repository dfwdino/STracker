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
    public class STIREsultsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: STIREsults
        public ActionResult Index()
        {
            var sTIREsults = db.STIREsults.Include(s => s.Person);
            return View(sTIREsults.ToList());
        }

        // GET: STIREsults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STIREsult sTIREsult = db.STIREsults.Find(id);
            if (sTIREsult == null)
            {
                return HttpNotFound();
            }
            return View(sTIREsult);
        }

        // GET: STIREsults/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name");
            return View();
        }

        // POST: STIREsults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,STI,Result,PersonID,Deleted,ResultsDate")] STIREsult sTIREsult)
        {
            if (ModelState.IsValid)
            {
                db.STIREsults.Add(sTIREsult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", sTIREsult.PersonID);
            return View(sTIREsult);
        }

        // GET: STIREsults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STIREsult sTIREsult = db.STIREsults.Find(id);
            if (sTIREsult == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", sTIREsult.PersonID);
            return View(sTIREsult);
        }

        // POST: STIREsults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,STI,Result,PersonID,Deleted,ResultsDate")] STIREsult sTIREsult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTIREsult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.People, "ID", "Name", sTIREsult.PersonID);
            return View(sTIREsult);
        }

        // GET: STIREsults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STIREsult sTIREsult = db.STIREsults.Find(id);
            if (sTIREsult == null)
            {
                return HttpNotFound();
            }
            return View(sTIREsult);
        }

        // POST: STIREsults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STIREsult sTIREsult = db.STIREsults.Find(id);
            db.STIREsults.Remove(sTIREsult);
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
