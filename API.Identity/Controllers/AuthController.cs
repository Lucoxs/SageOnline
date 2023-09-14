using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;

namespace API.Identity.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken(string client_id, string code)
        {
            try
            {
                var response = await _authService.SignIn(client_id, code);
                return StatusCode((int)response.HttpStatusCode, response.Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> GetRefreshToken(string client_id, string refresh_token)
        {
            try
            {
                var response = await _authService.RefreshToken(client_id, refresh_token);
                return StatusCode((int)response.HttpStatusCode, response.Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo(string token)
        {
            try
            {
                var response = await _authService.GetUserInfoAsync(token);
                return StatusCode((int)response.HttpStatusCode, response.Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signout")]
        public async Task<IActionResult> Signout([FromBody] Signout signout)
        {
            var accessTokenResponse = await _authService.RevokeAccessToken(signout.client_id, signout.access_token);
            var refreshTokenResponse = await _authService.RevokeRefreshToken(signout.client_id, signout.refresh_token);
            if (!accessTokenResponse.IsError && !refreshTokenResponse.IsError)
                return Ok();
            return BadRequest();
        }
    }
}
