using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STracker.Infrastructure
{
    public class PageViewLoggingAttribute : ActionFilterAttribute
    {
        private static readonly TimeSpan pageViewDumpToDatabaseTimeSpan = new TimeSpan(0, 0, 10);

        //After the action method is called and before the result is executed (before view render).
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //var test = "";
        }

        //just before the action method is called.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            STrackerEntities _se = new STrackerEntities();

            Logging myLogging = new Logging();

            myLogging.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            myLogging.ActionName = filterContext.ActionDescriptor.ActionName;
            myLogging.Date = TimeZoneInfo.ConvertTime(filterContext.HttpContext.Timestamp, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).Date;
            myLogging.Time = TimeZoneInfo.ConvertTime(filterContext.HttpContext.Timestamp, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")).TimeOfDay;
            myLogging.IPAddress = filterContext.HttpContext.Request.UserHostAddress;
            myLogging.ActionParameters = "";

            myLogging.AbsoluteUri = filterContext.HttpContext.Request.UrlReferrer?.AbsoluteUri;


            foreach (var pram in filterContext.ActionParameters)
            {
                myLogging.ActionParameters += pram + " ";
            }

            myLogging.ActionParameters = myLogging.ActionParameters.Trim();

            _se.Loggings.Add(myLogging);
            try
            {
                _se.SaveChanges();
            }
            catch
            {
                return;

            }

        }

        //OnResultExecuting(ResultExecutingContext filterContext) : Just before the result is executed(before view render).
        //OnResultExecuted(ResultExecutedContext filterContext) : After the result is executed(after the view is rendered).



    }
}