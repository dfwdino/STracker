using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class HoleModel
    {
        public int ID { get; set; }
        public string Area { get; set; }
        public bool Deleted { get; set; }
        public List<int> SelectedHoles { get; set; }
    }
}