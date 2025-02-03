using System;
using System.ComponentModel.DataAnnotations.Schema;
using CrossCutting.Enums;
namespace CrossCutting.Models
{
	public class Employer : BaseEntity
    {
		public Employer()
		{
			
			BirthDate = DateTime.UtcNow;

		}
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string DocNumber { get; set; }
		public Role Role { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
	}
}

