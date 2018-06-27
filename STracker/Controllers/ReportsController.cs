using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STracker.Controllers
{
    public class ReportsController : Controller
    {

        private STrackerEntities db = new STrackerEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SumAll() {

            dynamic SumAll = db.EventDetails
                                .Join(db.EventActions,cr=>cr.ActionDone,bn => bn.ID,(cr,bn) => new {cr,bn })
                                 .GroupBy(m => m.bn.Name)
                                .Select(g => new {Name = g.Key, TotalNumber = g.Count() })
                                .OrderBy(x => x.TotalNumber);


            return View(SumAll);
        }


    }
}