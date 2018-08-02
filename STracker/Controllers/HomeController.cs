using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web.Routing;
using System.Text;
using System.Security.Cryptography;
using System.IO;

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
            Login user = db.Logins.Where(m => m.UserName == login.UserName).SingleOrDefault();
          
            if (user != null)
            {
                if (!VerifyPassword(login.Password,user.Hash,user.Salt))
                {
                    ModelState.AddModelError(login.UserName, "Can't find user");
                    return View();
                }

                HttpCookie siteCookie = new HttpCookie("Stacking");

                siteCookie.Values.Add("HasAccess", "true");
                siteCookie.Values.Add("ID", user.ID.ToString());
                siteCookie.Expires = DateTime.Now.Date.AddDays(7);
                this.ControllerContext.HttpContext.Response.Cookies.Add(siteCookie);
                  
                return RedirectToAction("Index","Events");
            }

           

            return View();
        }

       public bool VerifyPassword(string password, string hash, string salt){
            var saltBytes = Convert.FromBase64String(salt);
            var rfc289 = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            return Convert.ToBase64String(rfc289.GetBytes(256)) == hash;
        }



        public static HttpRequest GetHttpRequest()
        {
            return System.Web.HttpContext.Current.Request;
        }
        
    }
}