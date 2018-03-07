
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace STracker.Models
{
	public class LocationsCreate
    {	
        
        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Owner ID")]
        public Nullable<int> OwnerID { get; set; }

        public List<int> SelectedLocations { get; set; }

    }
}


