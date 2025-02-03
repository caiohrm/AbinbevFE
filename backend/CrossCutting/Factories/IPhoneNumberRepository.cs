using System;
using CrossCutting.Models;
using CrossCutting.ViewModels.PhoneNumber;

namespace CrossCutting.Factories
{
	public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumber>> GetPhoneNumbers(int employerId);
        Task<bool> PostPhoneNumbers(PhoneNumber number,CancellationToken token=default);
        Task<bool> PutPhoneNumbers(PhoneNumber phoneNumber,CancellationToken token=default);
        Task<bool> DeletePhoneNumbers(int employerId, int phoneNumberId,CancellationToken token=default);
    }
}

