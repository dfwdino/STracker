using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class PeopleDetailMode
    {
        public PeopleDetailMode() {
            TheyDid = new List<ThingsDoneModel>();
            WasDone = new List<ThingsDoneModel>();
        }

        public string Name { get; set; }
        public string Notes { get; set; }

        [DisplayName("Where did you Meet Them")]
        public string WhereDidYouMeetThem { get; set; }

        public string photoURL { get; set; }

        public List<SocalSite> SocialSites { get; set; }

        public List<STIREsult> STIResults { get; set; }

        public List<ThingsDoneModel> TheyDid { get; set; }

        public List<ThingsDoneModel> WasDone { get; set; }
    }
}