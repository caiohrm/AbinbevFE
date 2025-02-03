using System;
using CrossCutting.Models;
using CrossCutting.ViewModels.Employers;

namespace CrossCutting.Services
{
	public interface IEmployerService
	{
	    Task<IEnumerable<Employer>> GetEmployers(int page, int count,string document);
		Task<AddEmployerResponse> GetEmployer(int id);
		Task<AddEmployerResponse> AddEmployer(AddEmployerRequest request);
		Task<bool> UpdateEmployer(UpdateEmployerRequest request,int employerId);
		Task<bool> DeleteEmployer(int employerId);
    }
}

