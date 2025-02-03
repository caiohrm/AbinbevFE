using System;
using CrossCutting.Services;
using CrossCutting.ViewModels.Authentication;
using CrossCutting.ViewModels.Employers;
using Microsoft.AspNetCore.Mvc;

namespace AbInbev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticate;
        public AuthenticationController(IAuthenticateService authenticateService)
		{
            _authenticate = authenticateService;
		}

        [HttpPost("")]
        public async Task<IActionResult> Authenticate([FromBody] AutenticateRequest request)
        {
            var validator = request.Validate();
            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors); 
            }

            var authentication = await _authenticate.Authenticate(request);
            if (authentication == null)
                return Unauthorized();
                
            return Ok(authentication);

        }

    }
}

