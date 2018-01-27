using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STracker.Models
{
    public class CreateEvent
    {
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        
        public int OverAllRating { get; set; }

        public int OrgamNumber { get; set; }

        public IList<CreateEventDetail> EventDetails { get; set; }
        
        public IList<SelectListItem> ListToWho { get; set; }

        public IList<CreateFuckingList> Fucks { get; set; }


    }
}