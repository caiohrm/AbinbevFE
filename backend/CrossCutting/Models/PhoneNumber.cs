using System;
namespace CrossCutting.Models
{
	public class PhoneNumber : BaseEntity
    {
		public string CountryCode { get; set; }
		public string RegionCode { get; set; }
		public string Number { get; set; }
		public int EmployerId { get; set; }
		
	}
}

