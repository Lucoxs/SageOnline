using API.Identity.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;

namespace API.Identity.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet("token")]
        public async Task<IActionResult> GetToken(string client_id, string code)
        {
            var baseUri = new Uri(Request.IsHttps ? $"https://{Request.Host.Value}" : $"http://{Request.Host.Value}");
            using var httpClient = new HttpClient();
            try 
            {
                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new(baseUri, "/connect/token"),
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "client_id", client_id },
                        { "client_secret", client_id },
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", "http://localhost:3000/signin-oidc" }
                    })
                };

                using var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadFromJsonAsync<dynamic>();
                if (response.IsSuccessStatusCode)
                    return Ok(responseContent);
                return BadRequest(responseContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken(string client_id, string refresh_token)
        {
            var baseUri = new Uri(Request.IsHttps ? $"https://{Request.Host.Value}" : $"http://{Request.Host.Value}");
            using var httpClient = new HttpClient();

            try
            {
                HttpRequestMessage request = new()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new(baseUri, "/connect/token"),
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "client_id", client_id },
                        { "client_secret", client_id },
                        { "grant_type", "refresh_token" },
                        { "refresh_token", refresh_token }
                    })
                };

                using var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadFromJsonAsync<dynamic>();
                if (response.IsSuccessStatusCode)
                    return Ok(responseContent);
                return BadRequest(responseContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
