using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class CreateEventDetail
    {
        

        public int ID { get; set; }
        
        public int EventID { get; set; }
        
        [Required]
        public int? WhoDid { get; set; }
        
        public int ActionDone { get; set; }

        [Required]
        public int? ToWho { get; set; }
        
        [Required]
        public List<int> SelectedAction { get; set; }
    }
}