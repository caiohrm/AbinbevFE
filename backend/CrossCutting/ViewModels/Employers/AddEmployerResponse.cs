using System;
using CrossCutting.Enums;
using FluentValidation;

namespace CrossCutting.ViewModels.Employers
{
	public class AddEmployerResponse
	{
		public AddEmployerResponse()
		{

		}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocNumber { get; set; }
        public Role Role { get; set; }
        public DateTime BirthDate { get; set; }
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
      
    }

    
}

