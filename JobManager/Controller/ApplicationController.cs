using JobManager.Controller.Base;
using JobManager.Core.Data.DataTransferObjects.Request.Authentication;
using JobManager.Core.Data.DataTransferObjects.Request.Registration;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Controller
{
    [Route("api/authentication")]
    public class ApplicationController : BaseController
    {
        private readonly IUserService _userService;

        public ApplicationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponseDto>> Authenticate(AuthenticationRequestDto authenticationRequestDto)
        {
            var response = await _userService.Authenticate(authenticationRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponseDto>> Register(RegistrationRequestDto registrationRequestDto)
        {
            var response = await _userService.Register(registrationRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
