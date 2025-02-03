using System;
using CrossCutting.Models;
using CrossCutting.ViewModels.PhoneNumber;

namespace CrossCutting.Services
{
	public interface IPhoneNumberService
	{
		Task<IEnumerable<PhoneNumber>> GetPhoneNumbers(int employerId);
		Task<bool> PostPhoneNumbers(int employerId, AddPhoneNumberRequest request);
        Task<bool> PutPhoneNumbers(int employerId,int phoneNumberId ,UpdatePhoneNumberRequest request);
        Task<bool> DeletePhoneNumbers(int employerId,int phoneNumberId);

    }
}

