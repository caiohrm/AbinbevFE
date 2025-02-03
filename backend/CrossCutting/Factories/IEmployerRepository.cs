using System;
using CrossCutting.Models;
using CrossCutting.ViewModels;

namespace CrossCutting.Factories
{
	public interface IEmployerRepository
	{
        Task<IEnumerable<Employer>> GetEmployers(int page, int count,string document);
        Task<Employer?> GetEmployer(int id);
        Task<Employer> AddEmployer(Employer employer, CancellationToken cancellationToken = default);
        Task<bool> UpdateEmployer(Employer employer, CancellationToken cancellationToken = default);
    }
}

