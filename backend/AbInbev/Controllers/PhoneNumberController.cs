using System;
using CrossCutting.Models;
using CrossCutting.Services;
using Microsoft.AspNetCore.Mvc;
using CrossCutting.ViewModels.PhoneNumber;
using Application.Services;

namespace AbInbev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberService _phoneNumberService;

        public PhoneNumberController(IPhoneNumberService phoneNumberService)
		{
            this._phoneNumberService = phoneNumberService;
        }


        [HttpGet("{employerId}")]
        public async Task<IActionResult> Get([FromRoute] int employerId)
        {
            try
            {
                return Ok(await _phoneNumberService.GetPhoneNumbers(employerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost("{employerId}")]
        public async Task<IActionResult> Post([FromRoute] int employerId, [FromBody] AddPhoneNumberRequest request)
        {
            try
            {
                var validator = request.Validate();
                if (!validator.IsValid)
                {
                    return BadRequest(validator.Errors);
                }
                return Ok(await _phoneNumberService.PostPhoneNumbers(employerId, request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPut("{employerId}/{phoneId}")]
        public async Task<IActionResult> Put([FromRoute] int employerId, [FromRoute] int phoneId, [FromBody] UpdatePhoneNumberRequest request)
        {
            try
            {
                var validator = request.Validate();
                if (!validator.IsValid)
                {
                    return BadRequest(validator.Errors);
                }

                var response = await _phoneNumberService.PutPhoneNumbers(employerId, phoneId, request);
                return response ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{employerId}/{phoneId}")]
        public async Task<IActionResult> Delete([FromRoute] int employerId, [FromRoute] int phoneId)
        {
            try
            {
                var response = await _phoneNumberService.DeletePhoneNumbers(employerId, phoneId);
                return response ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

