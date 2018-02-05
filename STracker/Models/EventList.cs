using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class EventList
    {
        public int ID { get; set; }

        public DateTime EventDate { get; set; }

        public string Notes { get; set; }

        public int? OverAllRating { get; set; }

        public int? OrgamNumber { get; set; }

        public List<EventListDetails> EventActs { get; set; }
        public List<FuckingList> Fucks { get; set; }
        public List<HoleUsed> HolesUsed { get; set; }
    }
}