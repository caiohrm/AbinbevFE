using System;
using Application.Commom;
using CrossCutting.Commom;
using CrossCutting.Services;
using CrossCutting.ViewModels.Authentication;

namespace Application.Services
{
	public class AuthenticateService : IAuthenticateService
    {
        private readonly IEmployerService _employerService;
        private readonly IJwtManager _jwtManager;

        public AuthenticateService(IEmployerService employerService, IJwtManager jwtManager)
		{
            _employerService = employerService;
            this._jwtManager = jwtManager;
        }

        public async Task<AuthenticateResponse> Authenticate(AutenticateRequest request)
        {
            var employers = await _employerService.GetEmployers(1, 5, request.Document);
            var employer = employers.FirstOrDefault();
            if (employer == null || !employer.Enabled)
                return null;

            if (!HashManager.ValidateHash(employer.Password, request.Password))
                return null;

            var token = _jwtManager.GenerateToken(employer);

            return new AuthenticateResponse()
            {
                Token = token,
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                Email = employer.Email,
                DocNumber = employer.DocNumber,
                Role = employer.Role,
                BirthDate = employer.BirthDate,
                Id = employer.Id,
                Enabled = employer.Enabled,
                CreatedDate = employer.CreatedDate,
                UpdatedDate = employer.UpdatedDate
            };

        }
    }
}

