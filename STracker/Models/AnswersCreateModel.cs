using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace STracker.Models
{
    public class AnswersCreateModel
    {
        public int PersonID { get; set; }
        public List<AskedQuestion> Questions { get; set; }
        public List<string> Answer { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }
    }
}