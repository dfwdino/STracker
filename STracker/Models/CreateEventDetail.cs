using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class CreateEventDetail
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public int WhoDid { get; set; }
        public int ActionDone { get; set; }
        public int ToWho { get; set; }
        public List<int> SelectedAction { get; set; }
    }
}