using System;
using System.Collections.Generic;
using System.Threading;
using CrossCutting.Factories;
using CrossCutting.Models;
using CrossCutting.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class EmployerRepository : IEmployerRepository
	{
        private readonly DefaultContext _context;

        public EmployerRepository(DefaultContext context)
		{
            _context = context;
		}

        public async Task<Employer> AddEmployer(Employer employer,CancellationToken cancellationToken = default)
        {
            await _context.Employers.AddAsync(employer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employer;
        }

        public async Task<Employer?> GetEmployer(int id)
        {
            return _context.Employers.First(o => o.Id == id && o.Enabled);
        }

        public async Task<IEnumerable<Employer>> GetEmployers(int page, int count,string document="")
        {
            try
            {
                if (page == 0)
                    page = 1;

                if (count == 0)
                    count = int.MaxValue;

                var skip = (page - 1) * count;
                IQueryable<Employer> query = _context.Employers;
                query = query.Where(x => x.Enabled);
                if (!string.IsNullOrEmpty(document))
                    query = query.Where(x => x.DocNumber.StartsWith(document));
                return query.OrderBy(x=> x.Id).Skip(skip).Take(count).ToList();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
            
        }

        public async Task<bool> UpdateEmployer(Employer employer,CancellationToken cancellationToken =default)
        {
            employer.UpdatedDate = DateTime.UtcNow;
            var existingEmployer = _context.Employers.SingleOrDefault(e => e.Id == employer.Id);
            if (existingEmployer != null)
            {
                _context.Entry(existingEmployer).State = EntityState.Detached;
            }

            _context.Employers.Update(employer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}

