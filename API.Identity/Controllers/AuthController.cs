using API.Identity.Context;
using API.Identity.Interfaces;
using API.Identity.Models;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Serilog;
using System;
using Token = API.Identity.Models.Token;

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
                Log.Debug("authhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
                Log.Debug(client_id);
                Log.Debug(code);
                var response = await _authService.SignIn(client_id, code);
                var responseString = await response.Content.ReadAsStringAsync();
                Log.Debug(response.IsSuccessStatusCode.ToString());
                Log.Debug(responseString);
                if (response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<Token>(responseString));
                else
                    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ErrorToken>(responseString));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace ?? "");
                Log.Error(ex.InnerException?.Message ?? "");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> GetRefreshToken(string client_id, string refresh_token)
        {
            try
            {
                var response = await _authService.RefreshToken(client_id, refresh_token);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<Token>(responseString));
                else
                    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<ErrorToken>(responseString));
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
                /*var responseString = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<object>(responseString));*/
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
