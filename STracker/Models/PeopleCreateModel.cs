using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class PeopleCreateModel
    {
        public string Name { get; set; }
        public string Notes { get; set; }

        [DisplayName("Where did you Meet Them")]
        public string WhereDidYouMeetThem { get; set; }

        public string photoURL { get; set; }

        public List<SocalSite> SocialSites { get; set; }

        
        public List<STIREsult> STIResults { get; set; }
    }
}