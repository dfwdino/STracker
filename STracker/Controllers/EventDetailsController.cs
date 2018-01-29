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
    public class EventDetailsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: EventDetails
        public ActionResult Index()
        {
            var eventDetails = db.EventDetails.Include(e => e.Event).Include(e => e.Person).Include(e => e.Person1).Include(e => e.EventAction);
            return View(eventDetails.ToList());
        }

        // GET: EventDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);
        }

        // GET: EventDetails/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "Notes");
            ViewBag.ToWho = new SelectList(db.People, "ID", "Name");
            ViewBag.WhoDid = new SelectList(db.People, "ID", "Name");
            ViewBag.ActionDone = new SelectList(db.EventActions, "ID", "Name");
            ViewBag.ListActionDone = db.EventActions.ToList<EventAction>();

            EventDetail ed = new EventDetail();
            //ed.ListActionDone = db.EventActions.ToList<Action>();
            return View(ed);
        }

        // POST: EventDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventID,WhoDid,ActionDone,ToWho")] EventDetail eventDetail, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.EventDetails.Add(eventDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "Notes", eventDetail.EventID);
            ViewBag.ToWho = new SelectList(db.People, "ID", "Name", eventDetail.ToWho);
            ViewBag.WhoDid = new SelectList(db.People, "ID", "Name", eventDetail.WhoDid);
            ViewBag.ActionDone = new SelectList(db.EventActions, "ID", "Name", eventDetail.ActionDone);
            return View(eventDetail);
        }

        // GET: EventDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Notes", eventDetail.EventID);
            ViewBag.ToWho = new SelectList(db.People, "ID", "Name", eventDetail.ToWho);
            ViewBag.WhoDid = new SelectList(db.People, "ID", "Name", eventDetail.WhoDid);
            ViewBag.ActionDone = new SelectList(db.EventActions, "ID", "Name", eventDetail.ActionDone);
            return View(eventDetail);
        }

        // POST: EventDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventID,WhoDid,ActionDone,ToWho")] EventDetail eventDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "Notes", eventDetail.EventID);
            ViewBag.ToWho = new SelectList(db.People, "ID", "Name", eventDetail.ToWho);
            ViewBag.WhoDid = new SelectList(db.People, "ID", "Name", eventDetail.WhoDid);
            ViewBag.ActionDone = new SelectList(db.EventActions, "ID", "Name", eventDetail.ActionDone);
            return View(eventDetail);
        }

        // GET: EventDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);
        }

        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventDetail eventDetail = db.EventDetails.Find(id);
            db.EventDetails.Remove(eventDetail);
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
