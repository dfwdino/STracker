using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STracker.Controllers
{
    public class EventsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: Events
        public ActionResult Index()
        {
            List<Models.EventList> listEL = new List<Models.EventList>();
            
            foreach (var stevent in db.Events.OrderBy(m => m.ID))
            {
                Models.EventList el = new Models.EventList();
                el.EventActs = new List<Models.EventListDetails>();
                el.Fucks = new List<Models.FuckingList>();
                el.OverAllRating = stevent.OverAllRating;
                el.OrgamNumber = stevent.OrgamNumber;


                Models.EventListDetails eld = new Models.EventListDetails();
                Models.FuckingList fl = new Models.FuckingList();

                el.EventDate = stevent.Date;
                el.Notes = stevent.Notes;

                foreach (var item in stevent.EventDetails)
                {
                    if (item.Person1.Name.Equals(eld.Who) && item.Person.Name.Equals(eld.ToWho)) {
                        eld.DidWhat += eld.DidWhat.Length.Equals(0) ? item.Action.Name : ", " + item.Action.Name;
                    }
                    else
                    {
                        if (eld.ToWho != null)
                            {
                                el.EventActs.Add(eld);
                            }
                        eld = new Models.EventListDetails();
                        eld.Who = item.Person1.Name;
                        eld.ToWho = item.Person.Name;
                        eld.DidWhat = item.Action.Name;
                    }
                    
                }

                foreach (var item in stevent.Fuckings)
                {
                    if (item.Person1.Name.Equals(fl.TopPerson) && item.Person.Name.Equals(fl.BottonPerson))
                    {
                        fl.Poistion += fl.Poistion == null ? item.Position.Type : ", " + item.Position.Type;
                    }
                    else
                    {
                        if (fl.TopPerson != null)
                        {
                            el.Fucks.Add(fl);
                        }
                        fl = new Models.FuckingList();
                        fl.TopPerson = item.Person1.Name;
                        fl.BottonPerson = item.Person.Name;
                        fl.Poistion = item.Position.Type;
                    }
                    
                }

                el.EventActs.Add(eld);
                el.Fucks.Add(fl); 
                listEL.Add(el);
            }

            

            return View(listEL.OrderByDescending(m => m.EventDate).ToList());
        }

        public ActionResult Create()
        {

            Models.CreateEvent ce = new Models.CreateEvent();

            List<Person> people = db.People.ToList();
            people.Add(new Person() { ID = 0, Name = "--Select---" });
            
            ViewBag.People = people.OrderBy(m => m.ID).ToList();
            ViewBag.Actions = db.Actions.ToList().OrderBy(m => m.Name);
            ViewBag.Positions = db.Positions.ToList().OrderBy(m => m.Type);
            ViewBag.OneToTen = Enumerable.Range(0, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return View(ce);
        }

        [HttpPost]
        public ActionResult Create(Models.CreateEvent ce)
        {
            //ModelState.AddModelError("ce.Date", "some error message");

            if (ModelState.IsValid)
            {
                STracker.Event stEvent = new Event();

                stEvent.Date = ce.Date;
                stEvent.Notes = ce.Notes;
                stEvent.OrgamNumber = ce.OrgamNumber;
                stEvent.OverAllRating = ce.OverAllRating;

                db.Events.Add(stEvent);

                foreach (var item in ce.EventDetails)
                {
                    foreach (int actions in item.SelectedAction)
                    {
                        EventDetail ed = new EventDetail();

                        ed.WhoDid = item.WhoDid;
                        ed.ToWho = item.ToWho;
                        ed.ActionDone = actions;

                        stEvent.EventDetails.Add(ed);
                    }

                }

                foreach (var fuck in ce.Fucks)
                {
                    if (fuck.SelectedPosition != null)
                    {
                        foreach (int fuckposition in fuck.SelectedPosition)
                        {
                            Fucking f = new Fucking();

                            f.TopPerson = fuck.TopPerson;
                            f.BottomPerson = fuck.BottonPerson;
                            f.PoistionID = fuckposition;

                            stEvent.Fuckings.Add(f);
                        }
                    }
                }
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            List<Person> people = db.People.ToList();
            people.Add(new Person() { ID = 0, Name = "--Select---" });
            ViewBag.People = people.OrderBy(m => m.ID).ToList();
            ViewBag.Actions = db.Actions.ToList().OrderBy(m => m.Name);
            ViewBag.Positions = db.Positions.ToList().OrderBy(m => m.Type);

            ViewBag.SelectedActions = ce.EventDetails;
            ViewBag.SelectedPositions = ce.Fucks;


            ViewBag.OneToTen = Enumerable.Range(0, 11).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return View(ce);
        }
    }
}