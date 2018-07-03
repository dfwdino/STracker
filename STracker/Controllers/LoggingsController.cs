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

            var returnlogging = db.Loggings
                                        .Where(m => m.IPAddress != "::1")
                                        .OrderBy(m => m.ID);

            return View(returnlogging);
        }


        public void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
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
