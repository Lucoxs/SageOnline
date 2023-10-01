using API.Identity.Context;
using API.Identity.DAO;
using API.Identity.DTO.User;
using API.Identity.Interfaces;
using API.Identity.Models;
using API.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Identity.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CompanyService _companyService;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;


        public UsersController(CompanyService companyService, UserService userService, UserManager<User> userManager, IAuthService authService)
        {
            _companyService = companyService;
            _userService = userService;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet("api/v1/Companies/{company_id}/[controller]")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(int company_id)
        {
            try
            {
                var company = await _companyService.GetCompany(company_id);
                if (company == null)
                    return NotFound("Company not found!");

                var users = await _userService.GetUsers(company);

                List<UserDTO> result = new();
                foreach(var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    result.Add(new UserDTO(user, roles.First()));
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        [HttpGet("api/v1/Companies/me/[controller]")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);
                return await GetUsers(int.Parse(tokenClaim.CompanyId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        [HttpGet("api/v1/Companies/{company_id}/[controller]/{user_id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int company_id, string user_id)
        {
            try
            {
                var company = await _companyService.GetCompany(company_id);
                if (company == null)
                    return NotFound("Company not found!");
                
                var user = await _userService.GetUser(company, user_id);
                if (user == null)
                    return NotFound("User not found!");
                
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(new UserDTO(user, roles.First()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        [HttpGet("api/v1/Companies/me/[controller]/me")]
        public async Task<ActionResult<UserDTO>> GetUserMe()
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);
                return await GetUser(int.Parse(tokenClaim.CompanyId), tokenClaim.UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        [HttpPost("api/v1/Companies/me/[controller]")]
        public async Task<ActionResult<UserDTO>> PostUser([FromBody] UserNewDTO userNewDTO)
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);
                var companyId = int.Parse(tokenClaim.CompanyId);

                var company = await _companyService.GetCompany(companyId);
                if (company == null)
                    return NotFound("company doesn't exist");

                if (await _userService.ExistUserByEmail(company, userNewDTO.Email))
                    return BadRequest("user already exist");

                User user = new(userNewDTO, company);
                var result = await _userService.CreateUser(user);
                if (!result.Succeeded)
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.Code} : {x.Description}")));

                var roles = await _userManager.GetRolesAsync(user);

                return CreatedAtAction("GetUser", new { company_id = companyId, user_id = user.Id }, new UserDTO(user, roles.First()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        [HttpPut("api/v1/Companies/me/[controller]/me")]
        [SwaggerResponse(204)]
        public async Task<IActionResult> PutUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);

                var companyId = int.Parse(tokenClaim.CompanyId);
                if (companyId != userDTO.CompanyId || tokenClaim.UserId != userDTO.Id)
                    return BadRequest("invalid request");

                var company = await _companyService.GetCompany(companyId);
                if (company == null)
                    return NotFound("company doesn't exist");

                var user = await _userService.GetUser(company, tokenClaim.UserId);
                if (user == null)
                    return NotFound("user doesn't exist");

                var result = await _userService.UpdateUser(user, userDTO);
                if (!result.Succeeded)
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.Code} : {x.Description}")));

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        //TODO : Forgot password
        //TODO : Change password

        [HttpDelete("api/v1/Companies/{company_id}/[controller]/{user_id}")]
        [SwaggerResponse(204)]
        public async Task<IActionResult> DeleteUser(int company_id, string user_id)
        {
            try
            {
                var company = await _companyService.GetCompany(company_id);
                if (company == null)
                    return NotFound("Company not found!");
                
                var user = await _userService.GetUser(company, user_id);
                if (user == null)
                    return NotFound("User not found!");

                await _userService.Remove(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }
    }
}
