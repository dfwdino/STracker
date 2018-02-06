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
    public class AskedQuestionsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: AskedQuestions
        public ActionResult Index()
        {
            return View(db.AskedQuestions.ToList());
        }

        // GET: AskedQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AskedQuestion askedQuestion = db.AskedQuestions.Find(id);
            if (askedQuestion == null)
            {
                return HttpNotFound();
            }
            return View(askedQuestion);
        }

        // GET: AskedQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AskedQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Deleted,Hide,Question")] AskedQuestion askedQuestion)
        {
            if (ModelState.IsValid)
            {
                db.AskedQuestions.Add(askedQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(askedQuestion);
        }

        // GET: AskedQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AskedQuestion askedQuestion = db.AskedQuestions.Find(id);
            if (askedQuestion == null)
            {
                return HttpNotFound();
            }
            return View(askedQuestion);
        }

        // POST: AskedQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Deleted,Hide,Question")] AskedQuestion askedQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(askedQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(askedQuestion);
        }

        // GET: AskedQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AskedQuestion askedQuestion = db.AskedQuestions.Find(id);
            if (askedQuestion == null)
            {
                return HttpNotFound();
            }
            return View(askedQuestion);
        }

        // POST: AskedQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AskedQuestion askedQuestion = db.AskedQuestions.Find(id);
            askedQuestion.Deleted = true;
            //db.AskedQuestions.Remove(askedQuestion);
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
