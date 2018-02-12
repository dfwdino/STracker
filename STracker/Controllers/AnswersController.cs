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
using STracker.Models;

namespace STracker.Controllers
{
    public class AnswersController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        [StrackerAccess]
        public ActionResult Index()
        {
            IList<IGrouping<DateTime,AnsweredQuestion>> test = db.AnsweredQuestions.OrderByDescending(m => m.EventDate).Where(m => m.Deleted == false && m.Hide == false)
                                        .GroupBy(m => m.EntryDate).ToList();

            return View(test);
        }
        
        // GET: Answers/Create
        public ActionResult Create(string id)
        {
            AnswersCreateModel answer = new AnswersCreateModel();
            answer.Questions = db.AskedQuestions.Where(m => m.Deleted == false && m.Hide == false).ToList();
            answer.Answer = new List<string>();
            answer.EventDate = DateTime.Now;
            
            if(id != null)
                answer.PersonID = db.People.Where(m => m.Name == id).Single().ID;

            return View(answer);
        }

        public ActionResult GoodJob(){
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswersCreateModel answers)
        {
            DateTime dtnow = DateTime.Now;
        
            if (ModelState.IsValid)
            {
                for (int i = 0; i < answers.Answer.Count(); i++)
                {
                    AnsweredQuestion oneanswer = new AnsweredQuestion();
                    oneanswer.Answered = answers.Answer[i];
                    oneanswer.QuestionID = answers.Questions[i].ID;
                    oneanswer.PersonID = answers.PersonID;
                    oneanswer.EntryDate = dtnow;
                    oneanswer.EventDate = answers.EventDate;

                    db.AnsweredQuestions.Add(oneanswer);
                }
                
                
                db.SaveChanges();
                return RedirectToAction("GoodJob");
            }

            return View(answers);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AskedQuestion answers = db.AskedQuestions.Find(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] AnswersCreateModel answers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answers);
        }
        
        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnsweredQuestion answers = db.AnsweredQuestions.Find(id);
            db.AnsweredQuestions.Remove(answers);
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
