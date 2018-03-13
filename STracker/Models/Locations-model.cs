
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace STracker.Models
{
	public class Locations
	{

		public Locations()
		{
			
		}


[Required]
[DisplayName("ID")]
public int ID { get; set; }

[Required]
[DisplayName("Deleted")]
public bool Deleted { get; set; }

[Required]
[DisplayName("Name")]
public string Name { get; set; }

[DisplayName("Owner ID")]
public int OwnerID { get; set; }

}}


