using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STracker;
using STracker.Infrastructure;

namespace STracker.Controllers
{
    [StrackerAccess]
    public class EventActionsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: EventActions
        public ActionResult Index()
        {
            return View(db.EventActions.Where(m => m.Deleted == false).OrderBy(m => m.Name).ToList());
        }

        // GET: EventActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventAction action = db.EventActions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: EventActions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] EventAction eventaction)
        {
            if (ModelState.IsValid)
            {
                eventaction.OwnerID = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);
                db.EventActions.Add(eventaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventaction);
        }

        // GET: EventActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventAction action = db.EventActions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: EventActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] EventAction eventaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventaction);
        }

        // GET: EventActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventAction action = db.EventActions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: EventActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventAction action = db.EventActions.Find(id);
            action.Deleted = true;
            //db.EventActions.Remove(action);
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
