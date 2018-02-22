using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web.Routing;

namespace STracker.Controllers
{
    public class HomeController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: Home
        public ActionResult Index()
        {

            HttpCookie cookie = GetHttpRequest().Cookies["Stacking"];

            if (cookie != null || cookie?.HasKeys == true)
            {
                return RedirectToAction("Index","Events",null);
            }

                return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {

            Login user = db.Logins.Where(m => m.UserName == login.UserName && m.Password == login.Password).SingleOrDefault();
            if (user != null)
            {
                HttpCookie siteCookie = new HttpCookie("Stacking");

                siteCookie.Values.Add("HasAccess", "true");
                siteCookie.Values.Add("ID", user.ID.ToString());
                siteCookie.Expires = DateTime.Now.Date.AddDays(1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(siteCookie);
                  
                return RedirectToAction("Index","Events");
            }

            ModelState.AddModelError(login.UserName, "Can't find user");

            return View();
        }

        public static HttpRequest GetHttpRequest()
        {
            return System.Web.HttpContext.Current.Request;
        }
        
    }
}