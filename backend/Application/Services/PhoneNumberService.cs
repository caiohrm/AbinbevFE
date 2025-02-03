using System;
using AutoMapper;
using CrossCutting.Factories;
using CrossCutting.Models;
using CrossCutting.Services;
using CrossCutting.ViewModels.PhoneNumber;

namespace Application.Services
{
	public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IPhoneNumberRepository _phoneNumberRepository;
        private readonly IMapper _mapper;

        public PhoneNumberService(IPhoneNumberRepository phoneNumberRepository, IMapper mapper)
		{
            this._phoneNumberRepository = phoneNumberRepository;
            this._mapper = mapper;
        }

        

        public Task<bool> DeletePhoneNumbers(int employerId, int phoneNumberId)
        {
            return _phoneNumberRepository.DeletePhoneNumbers(employerId, phoneNumberId);
        }

        public Task<IEnumerable<PhoneNumber>> GetPhoneNumbers(int employerId)
        {
            return _phoneNumberRepository.GetPhoneNumbers(employerId);
        }

        public Task<bool> PostPhoneNumbers(int employerId, AddPhoneNumberRequest request)
        {
            
            var phoneNumber = _mapper.Map<PhoneNumber>(request, opt=>
            {
                opt.Items.Add("employerId", employerId);
            });

            return _phoneNumberRepository.PostPhoneNumbers(phoneNumber);
        }

        public Task<bool> PutPhoneNumbers(int employerId, int phoneNumberId, UpdatePhoneNumberRequest request)
        {
            var phoneNumber = _mapper.Map<PhoneNumber>(request, opt =>
            {
                opt.Items.Add("employerId", employerId);
                opt.Items.Add("phoneNumberId", phoneNumberId);
            });
            return _phoneNumberRepository.PutPhoneNumbers(phoneNumber);
        }
    }
}

