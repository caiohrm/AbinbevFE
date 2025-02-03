using System;
using CrossCutting.Factories;
using CrossCutting.Models;
using Infrastructure.Repositories;
using CrossCutting.Services;
using CrossCutting.ViewModels.Employers;
using AutoMapper;
using Application.Commom;

namespace Application.Services
{
	public class EmployerService : IEmployerService
	{
		private IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;

        public EmployerService(IEmployerRepository employerRepository, IMapper mapper)
		{
			_employerRepository = employerRepository;
            _mapper = mapper;
        }

        public async Task<AddEmployerResponse> AddEmployer(AddEmployerRequest request)
        {
            var employer = _mapper.Map<Employer>(request);
            var result = await _employerRepository.GetEmployers(1, 1, employer.DocNumber);
            if (result.Any())
                throw new Exception("User with the same documents exist's");

            employer.Password = HashManager.GetStringHash(employer.Password);
            var response = await _employerRepository.AddEmployer(employer);
            return _mapper.Map<AddEmployerResponse>(response);
        }

        public async Task<AddEmployerResponse> GetEmployer(int id)
        {
            var response = await _employerRepository.GetEmployer(id);
            return response == null ?null :_mapper.Map<AddEmployerResponse>(response);
        }

        public async Task<IEnumerable<Employer>> GetEmployers(int page, int count,string document="")
		{
			return await _employerRepository.GetEmployers(page,count,document);
		}

        public async Task<bool> UpdateEmployer(UpdateEmployerRequest request,int employerId)
        {
            var employer = _mapper.Map<Employer>(request, opt =>
            {
                opt.Items.Add("employerId", employerId);
            });

            var result = (await GetEmployers(1, 1, employer.DocNumber))?.FirstOrDefault();
            if (result == null)
                return false;

            if (result.Id != employer.Id)
                throw new Exception("User with the same documents exist's");
            _mapper.Map(request, result, opt =>
            {
                opt.Items.Add("employerId", employerId);
            });
            return await _employerRepository.UpdateEmployer(result);
        }

        public async Task<bool> DeleteEmployer(int employerId)
        {
            var employer = await _employerRepository.GetEmployer(employerId);
            if (employer == null)
                throw new  Exception("Employer doesn't exists");

            employer.Disable();

            return await _employerRepository.UpdateEmployer(employer);

        }

        
    }
}

