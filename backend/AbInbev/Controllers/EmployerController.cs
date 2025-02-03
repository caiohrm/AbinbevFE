using System;
using Microsoft.AspNetCore.Mvc;
using CrossCutting.Models;
using CrossCutting.Services;
using CrossCutting.ViewModels.Employers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CrossCutting.Enums;

namespace AbInbev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly ILogger<EmployerController> _logger;
        private readonly IEmployerService _employerService;

        public EmployerController(ILogger<EmployerController> logger,IEmployerService employerService)
		{
            _logger = logger;
            _employerService = employerService;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]int page=1, [FromQuery] int count = 5, [FromQuery] string document="")
        {
            try
            {
                return Ok(await _employerService.GetEmployers(page, count, document));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(await _employerService.GetEmployer(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [Authorize(Roles ="2,3")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            try
            {
                return Ok(await _employerService.DeleteEmployer(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //[Authorize()]
        [HttpPost("")]
        public async Task<IActionResult> PostEmployer([FromBody] AddEmployerRequest request)
        {
            try
            {
                var role = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Role));
                if (role < (int)request.Role)
                    return BadRequest("Role not authorized");

                var validator = request.Validate();
                if (!validator.IsValid)
                {
                    return BadRequest(validator.Errors);
                }
                return Ok(await _employerService.AddEmployer(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [Authorize()]
        [HttpPut("{id}")]
        public async Task<IActionResult> PustEmployer([FromBody] UpdateEmployerRequest request, [FromRoute] int id)
        {
            try
            {
                var role = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Role));
                if (role < (int)request.Role)
                    return BadRequest("Role not authorized");

                var validator = request.Validate();
                if (!validator.IsValid)
                {
                    return BadRequest(validator.Errors); // Retorna erros de validação automaticamente
                }
                return Ok(await _employerService.UpdateEmployer(request,id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }



    }
}

