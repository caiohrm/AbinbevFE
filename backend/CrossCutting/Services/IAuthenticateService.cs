using System;
using CrossCutting.ViewModels.Authentication;

namespace CrossCutting.Services
{
	public interface IAuthenticateService
	{
		Task<AuthenticateResponse> Authenticate(AutenticateRequest request);
	}
}

