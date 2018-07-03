using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace STracker.Models
{
	public class LoggingList
	{
		public LoggingList()
		{
			
		}

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("IP")]
        public string IPAddress { get; set; }

        [DisplayName("Controller")]
        public string ControllerName { get; set; }

        [DisplayName("Action")]
        public string ActionName { get; set; }

        [DisplayName("Parameters")]
        public string ActionParameters { get; set; }

        [DisplayName("Uri")]
        public string AbsoluteUri { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Time")]
        public DateTime Time { get; set; }

}}


