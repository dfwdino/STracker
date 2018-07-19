using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class FuckingList
    {
        public string TopPerson { get; set; }
        public string Poistion { get; set; }
        public string BottonPerson { get; set; }
        public List<int> SelectedAction { get; set; }

        public bool CondomUsed { get;set; }
    }
}