using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class AnswersListModel
    {
        public string Name { get; set; }

        public List<string> Questions { get; set; }
        public List<string> Answer { get; set; }
        public DateTime EventDate { get; set; }
    }
}