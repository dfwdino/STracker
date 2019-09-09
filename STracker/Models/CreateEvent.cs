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
        public int ID { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        
        public int? OverAllRating { get; set; }

        public int OrgamNumber { get; set; }

        public int LoadSize { get; set; }

        public bool Squirt { get; set; }

        public IList<CreateEventDetail> EventDetails { get; set; }
        
        public IList<SelectListItem> ListToWho { get; set; }

        public IList<CreateFuckingList> Fucks { get; set; }
        
        public IList<LocationsCreate> Locations { get; set; }


    }
}