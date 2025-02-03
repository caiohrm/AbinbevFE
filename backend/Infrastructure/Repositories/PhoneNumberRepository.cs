using System;
using System.Threading;
using CrossCutting.Factories;
using CrossCutting.Models;
using CrossCutting.ViewModels.PhoneNumber;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly DefaultContext _context;

        public PhoneNumberRepository(DefaultContext context)
        {
            this._context = context;
        }


        public async Task<bool> DeletePhoneNumbers(int employerId, int phoneNumberId,CancellationToken token=default)
        {
            var phoneNumber = _context.PhoneNumbers.FirstOrDefault(x => x.EmployerId == employerId && x.Id == phoneNumberId);
            _context.PhoneNumbers.Remove(phoneNumber);
            await _context.SaveChangesAsync(token);
            return true;
        }

        public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbers(int employerId)
        {
            return _context.PhoneNumbers.Where(x => x.EmployerId == employerId).ToList();
        }

        public async Task<bool> PostPhoneNumbers(PhoneNumber phoneNumber,CancellationToken token=default)
        {
            _context.PhoneNumbers.Add(phoneNumber);
            await _context.SaveChangesAsync(token);
            return true;
        }

        public async Task<bool> PutPhoneNumbers(PhoneNumber phoneNumber, CancellationToken token)
        {
            _context.PhoneNumbers.Update(phoneNumber);
            await _context.SaveChangesAsync(token);
            return true;
        }
    }
}

