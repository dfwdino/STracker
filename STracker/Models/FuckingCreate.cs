using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class CreateFuckingList
    {
        public int TopPerson { get; set; }
        public string Poistion { get; set; }
        public int BottonPerson { get; set; }
        public List<int> SelectedPosition { get; set; }
    }
}