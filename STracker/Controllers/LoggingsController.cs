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
    public class LoggingsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: Loggings
        
        public ActionResult Index()
        {
            return View(db.Loggings.Where(m => m.IPAddress != "::1").OrderBy(m => m.ID).ToList());
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
