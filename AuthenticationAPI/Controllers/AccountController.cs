using JwtAuthenticationManager;
using JwtAuthenticationManager.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            this._jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = await _jwtTokenHandler.GenerateSecurityToken(authenticationRequest);
            if (authenticationResponse == null)
            {
                return Unauthorized();
            }
            return authenticationResponse;
        }
    }
}
