using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class CreateFuckingList
    {
        public CreateFuckingList()
        {
            CondomUsed = false;
        }


        public int ID { get; set; }
        public int TopPerson { get; set; }
        public string Poistion { get; set; }
        public int BottomPerson { get; set; }
        public List<int> SelectedPosition { get; set; }

        public bool CondomUsed { get; set; }
    }
}