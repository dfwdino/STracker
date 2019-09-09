using STracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STracker.Controllers
{
    [StrackerAccess]
    public class EventsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: Events
        public ActionResult Index()
        {
            List<Models.EventList> listEL = new List<Models.EventList>();

            DateTime dt = DateTime.Now.AddMonths(-1);
            int currentid = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            foreach (var stevent in db.Events.Where(m => m.Date >= dt && m.Deleted == false && m.OwnerID == currentid).OrderByDescending(m => m.ID))
            {
                Models.EventList el = new Models.EventList();
                el.EventActs = new List<Models.EventListDetails>();
                el.Fucks = new List<Models.FuckingList>();
                el.OverAllRating = stevent.OverAllRating;
                el.OrgamNumber = stevent.OrgamNumber;


                Models.EventListDetails eld = new Models.EventListDetails();
                Models.FuckingList fl = new Models.FuckingList();

                el.ID = stevent.ID;
                el.EventDate = stevent.Date;
                el.Notes = stevent.Notes;
                el.OrgamNumber = stevent.OrgamNumber;
                el.OverAllRating = stevent.OverAllRating;
                el.LoadSize = stevent.LoadSize;
                el.Squirt = stevent.Squirt;

                foreach (var item in stevent.EventDetails.Where(m => m.Deleted == false))
                {
                    if (item.Person1.Name.Equals(eld.Who) && item.Person.Name.Equals(eld.ToWho)) {
                        eld.DidWhat += eld.DidWhat.Length.Equals(0) ? ti.ToTitleCase(item.EventAction.Name) : ", " + ti.ToTitleCase(item.EventAction.Name);
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
                        eld.DidWhat = ti.ToTitleCase(item.EventAction.Name);
                    }
                    
                }

                foreach (var item in stevent.Fuckings.Where(m => m.Deleted == false))
                {
                    if (item.Person1.Name.Equals(fl.TopPerson) && item.Person.Name.Equals(fl.BottonPerson))
                    {
                        fl.Poistion += fl.Poistion == null ? ti.ToTitleCase(item.Position.Type) : ", " + ti.ToTitleCase(item.Position.Type);
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
                        fl.CondomUsed = item.CondomUsed == true ? true : false;
                    }
                    
                }

                foreach (var item in stevent.EventLocations.Where(m => m.Deleted == false))
                {
                    if(el.Locations == null)
                    {
                        el.Locations = ti.ToTitleCase(item.Location.Name);
                    }
                    else
                    {
                        el.Locations += ", " + ti.ToTitleCase(item.Location.Name);
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
            List<Person> people = db.People.ToList();
            people.Add(new Person() { ID = 0, Name = "--Select---" });

            int currentuserid = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);

            ViewBag.People = people.OrderBy(m => m.ID).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.EventAction = db.EventActions.OrderBy(m => m.Name).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.Positions = db.Positions.OrderBy(m => m.Type).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.Locations = db.Locations.OrderBy(m => m.Name).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.LoadSize = db.LoadSizes.OrderBy(m => m.Amount).OrderBy(m => m.ID).ToList();
            ViewBag.OneToTen = Enumerable.Range(0, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            //ViewBag.Holes = db.Holes.OrderBy(m => m.Area).ToList();

            return View(new Models.CreateEvent() {Date = DateTime.Now.AddDays(-1)});
        }

        private DateTime? FixFuckenDate(string datetime)
        {
            if (datetime.Length.Equals(0))
                return null;

            var newstr = new string(datetime.Where(c => c < 128).ToArray());

            return DateTime.Parse(newstr, new CultureInfo("en-US"), DateTimeStyles.None);
           
        }


        [HttpPost]
        public ActionResult Create(Models.CreateEvent ce)
        {
            int OwnerID = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);

            int EventDetailsPeople = ce.EventDetails.Where(m => m.ToWho == 0 || m.WhoDid == 0).Count();

           
            if (EventDetailsPeople > 0){
                ModelState.AddModelError("ce.EventDetails", "People are missing from the Details");
            }

            if (ce.Fucks != null)
            {
                int FuckPeople = ce.Fucks.Where(m => m.TopPerson == 0 || m.BottomPerson == 0).Count();
                if (!FuckPeople.Equals(0))
                {
                    ModelState.AddModelError("ce.EventDetails", "People are missing from the Fucking");
                }
            }


            if (ModelState.IsValid)
            {
                 STracker.Event stEvent = new Event();

                    stEvent.Date = Convert.ToDateTime(FixFuckenDate(ce.Date.ToString()));
                    stEvent.Notes = ce.Notes;
                    stEvent.OrgamNumber = ce.OrgamNumber;
                    stEvent.OverAllRating = ce.OverAllRating;
                    stEvent.LoadSize = ce.LoadSize;
                    stEvent.Squirt = ce.Squirt;
                    stEvent.OwnerID = OwnerID;


                    db.Events.Add(stEvent);
                    db.Entry(stEvent).State = EntityState.Added;

                    foreach (var item in ce.EventDetails)
                    {
                        foreach (int EventAction in item.SelectedAction)
                        {
                            EventDetail ed = new EventDetail();

                            ed.WhoDid = (int)item.WhoDid;
                            ed.ToWho = (int)item.ToWho;
                            ed.ActionDone = EventAction;

                            stEvent.EventDetails.Add(ed);
                        }

                    }

                if (ce.Fucks != null)
                {
                    foreach (var fuck in ce.Fucks)
                    {
                        if (fuck.SelectedPosition != null)
                        {
                            foreach (int fuckposition in fuck.SelectedPosition)
                            {
                                Fucking f = new Fucking();

                                f.TopPerson = fuck.TopPerson;
                                f.BottomPerson = fuck.BottomPerson;
                                f.PoistionID = fuckposition;
                                f.CondomUsed = fuck.CondomUsed;

                                stEvent.Fuckings.Add(f);
                            }
                        }
                    }
                }
                    if (ce.Locations != null)
                    {
                        foreach (var location in ce.Locations)
                        {
                            if (location.SelectedLocations != null)
                            {
                                foreach (int eventlocation in location.SelectedLocations)
                                {
                                    EventLocation el = new EventLocation();

                                    el.EventID = ce.ID;
                                    el.LocationID = eventlocation;

                                    stEvent.EventLocations.Add(el);
                                }
                            }
                        }
                    }

                    db.SaveChanges();
                    ModelState.Clear();
                
                return RedirectToAction("Index");
            }

            List<Person> people = db.People.ToList();
            people.Add(new Person() { ID = 0, Name = "--Select---" });
            ViewBag.People = people.OrderBy(m => m.ID).ToList();

            int currentuserid = Convert.ToInt16(Request.Cookies["Stacking"]["ID"]);
            ViewBag.EventAction = db.EventActions.OrderBy(m => m.Name).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.Positions = db.Positions.OrderBy(m => m.Type).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.Locations = db.Locations.OrderBy(m => m.Name).Where(m => m.Deleted == false && (m.OwnerID == currentuserid || m.OwnerID == null)).ToList();
            ViewBag.OneToTen = Enumerable.Range(0, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            
            ViewBag.SelectedEventAction = ce.EventDetails;

            ViewBag.SelectedPositions = ce.Fucks;


            ViewBag.OneToTen = Enumerable.Range(0, 11).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return View(ce);
        }

        public ActionResult Edit(int id) {

            Models.CreateEvent ce = new Models.CreateEvent();
            STracker.Event sexevent = db.Events.Where(m => m.ID == id).Single();

            ce.ID = sexevent.ID;
            ce.Date = sexevent.Date;
            ce.Notes = sexevent.Notes;
            ce.OrgamNumber = sexevent.OrgamNumber;
            ce.OverAllRating = sexevent.OverAllRating;

            List<Person> people = db.People.ToList();
            ce.EventDetails = new List<Models.CreateEventDetail>();
            ce.Fucks = new List<Models.CreateFuckingList>();

            foreach (var item in sexevent.EventDetails)
            {
                Models.CreateEventDetail ced = new Models.CreateEventDetail();
              
                ced.ToWho = item.ToWho;
                ced.WhoDid = item.WhoDid;
                ced.ActionDone = item.ActionDone;
                ced.ID = item.ID;

                ce.EventDetails.Add(ced);
            }

            foreach (var item in sexevent.Fuckings)
            {
                Models.CreateFuckingList ced = new Models.CreateFuckingList();

                ced.TopPerson = item.TopPerson;
                ced.BottomPerson = item.BottomPerson;
                ced.Poistion = item.Position.Type;
                ced.ID = item.ID;

                ce.Fucks.Add(ced);
            }

            ViewBag.People = people.OrderBy(m => m.ID).ToList();
            ViewBag.EventAction = db.EventActions.ToList().OrderBy(m => m.Name);
            ViewBag.Positions = db.Positions.ToList().OrderBy(m => m.Type);
            ViewBag.OneToTen = Enumerable.Range(0, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return View(ce);
        }

        [HttpPost]
        public ActionResult Edit(Models.CreateEvent ce)
        {
            var test = ce.EventDetails.First().WhoDid;

            Event sevent = db.Events.Where(x => x.ID == ce.ID).Single();

            sevent.EventDetails.ToList().ForEach(m => m.Deleted = true);
            sevent.Fuckings.ToList().ForEach(m => m.Deleted = true);

            //return View(ce);
            if (ModelState.IsValid)
            {
                STracker.Event sexevent = db.Events.Where(m => m.ID == ce.ID).Single();

                sexevent.ID = ce.ID;
                sexevent.Date = ce.Date;
                sexevent.Notes = ce.Notes;
                sexevent.OrgamNumber = ce.OrgamNumber;
                sexevent.OverAllRating = ce.OverAllRating;

                foreach (var item in ce.EventDetails)
                {
                    foreach (var selectedAction in item.SelectedAction)
                    {
                        EventDetail eventDetail = new EventDetail();

                        eventDetail.EventID = ce.ID;
                        eventDetail.WhoDid = (int)item.WhoDid ;
                        eventDetail.ToWho = (int)item.ToWho;
                        eventDetail.ActionDone = selectedAction;

                        sevent.EventDetails.Add(eventDetail);
                    }
                }

                if (ce.Fucks != null)
                {
                    foreach (var item in ce.Fucks)
                    {
                        foreach (var selectedPosition in item.SelectedPosition)
                        {
                            Fucking eventDetail = new Fucking();

                            eventDetail.EventID = ce.ID;
                            eventDetail.TopPerson = item.TopPerson;
                            eventDetail.BottomPerson = item.BottomPerson;
                            eventDetail.PoistionID = selectedPosition;

                            sevent.Fuckings.Add(eventDetail);
                        }
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }



    }
}