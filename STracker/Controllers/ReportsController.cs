using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STracker.Infrastructure;
using STracker.Models;

namespace STracker.Controllers
{
    [StrackerAccess]
    public class ReportsController : Controller
    {

        private STrackerEntities db = new STrackerEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SumAll() {

            List<SumAllViewModel> SumAll = db.EventDetails
                                .Join(db.EventActions,cr=>cr.ActionDone,bn => bn.ID,(cr,bn) => new {cr,bn })
                                 .GroupBy(m => m.bn.Name)
                                .Select(g => new Models.SumAllViewModel {Name = g.Key, TotalNumber = g.Count() })
                                .OrderByDescending(x => x.TotalNumber).ToList();

            //Really need to make this better by breaking up the model
            SumAll[0].TotalEvents = db.Events.Count();

            return View(SumAll);
        }

        public ActionResult WhoGotWhat()
        {

            List<Models.SumAllByPersonViewModel> SumAll = db.EventDetails
                                .Join(db.EventActions, ed => ed.ActionDone, ea => ea.ID, (ed, ea) => new { ed, ea })
                                 .GroupBy(m => new { m.ea.Name, m.ed.Person })
                                .Select(g => new Models.SumAllByPersonViewModel { ActionName = g.Key.Name,PersonName = g.Key.Person.Name, TotalNumber = g.Count() })
                                .OrderByDescending(x => x.TotalNumber).ToList();


            return View(SumAll);
        }

        public ActionResult WhoDidWhat()
        {

            List<SumAllByPersonViewModel> SumAll = db.EventDetails
                                .Join(db.EventActions, ed => ed.ActionDone, ea => ea.ID, (ed, ea) => new { ed, ea })
                                 .GroupBy(m => new { m.ea.Name, m.ed.Person1 })
                                .Select(g => new Models.SumAllByPersonViewModel { ActionName = g.Key.Name, PersonName = g.Key.Person1.Name, TotalNumber = g.Count() })
                                .OrderByDescending(x => x.TotalNumber).ToList();


            return View(SumAll);
        }


    }
}