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
    public class PeopleController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.OrderBy(m => m.Name).ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            Models.PeopleDetailMode tempperson = new Models.PeopleDetailMode();

            tempperson.ID = person.ID;
            tempperson.Name = person.Name;
            tempperson.Notes = person.Notes;
            tempperson.SocialSites = person.SocalSites.ToList();
            tempperson.STIResults = person.STIREsults.ToList();
            tempperson.WhereDidYouMeetThem = person.WhereDidYouMeetThem;
            

            var FromThem = db.EventDetails.Where(m => m.Person.ID == person.ID)
                                    .GroupBy(m => m.EventAction)
                                    .Select(n => new { Act = n.Key.Name, TimeDone = n.Count()}).ToList();

            foreach (var item in FromThem)
            {
                Models.ThingsDoneModel tempFromThem = new Models.ThingsDoneModel();

                tempFromThem.Act = item.Act;
                tempFromThem.TimeDone = item.TimeDone;

                tempperson.WasDone.Add(tempFromThem);
            }


            var ToThem = db.EventDetails.Where(m => m.Person1.ID == person.ID)
                                    .GroupBy(m => m.EventAction)
                                    .Select(n => new { Act = n.Key.Name, TimeDone = n.Count() }).ToList();

            foreach (var item in ToThem)
            {
                Models.ThingsDoneModel tempFromThem = new Models.ThingsDoneModel();

                tempFromThem.Act = item.Act;
                tempFromThem.TimeDone = item.TimeDone;

                tempperson.TheyDid.Add(tempFromThem);
            }


            return View(tempperson);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
           {
                Person tempperson = new Person();
                tempperson.Name = person.Name;
                tempperson.Notes = person.Notes;
                tempperson.WhereDidYouMeetThem = person.WhereDidYouMeetThem;
                tempperson.OwnerID = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);
                
                db.People.Add(tempperson);

                foreach (SocalSite item in person.SocalSites)
                {
                    tempperson.SocalSites.Add(item);
                }

                if (person.STIREsults.First().Person != null)
                {
                    foreach (STIREsult item in person.STIREsults)
                    {
                        tempperson.STIREsults.Add(item);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            var ss = db.SocalSites.Where(m => m.PersonID == id).ToList();

            return View(person);
        }

        public PartialViewResult PopUpSocialSite(int PersonID)
        {
            
            return PartialView(new SocalSite() {PersonID = PersonID });
        }


        [HttpPost]
        public ActionResult PopUpSocialSite(SocalSite socalSite)
        {
           
            db.SocalSites.Add(socalSite);
            db.SaveChanges();
           
            return View();
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Notes,WhereDidYouMeetThem")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            person.Deleted = !person.Deleted;
            //db.People.Remove(person);
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
