using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class PeopleCreateModel
    {
        public string Name { get; set; }
        public string Notes { get; set; }

        public List<SocalSite> SocalSites { get; set; }
    }
}