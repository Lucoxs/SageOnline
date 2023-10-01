using Microsoft.AspNetCore.Mvc;
using API.Identity.Context;
using API.Identity.Models;
using API.Identity.DAO;
using API.Identity.DTO.Company;
using API.Identity.Services;
using Swashbuckle.AspNetCore.Annotations;
using API.Identity.Interfaces;

namespace API.Identity.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly CompanyService _companyService;
        private readonly IAuthService _authService;

        public CompaniesController(AppDbContext context, CompanyService companyService, IAuthService authService)
        {
            _context = context;
            _companyService = companyService;
            _authService = authService;
        }

        // GET: api/v1/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyUserDTO>>> GetCompanies()
        {
            try
            {
                var companies = await _companyService.GetCompanies();
                return Ok(companies.Select(x => new CompanyUserDTO(x)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // GET: api/v1/Companies/5
        [HttpGet("{company_id}")]
        public async Task<ActionResult<CompanyUserDTO>> GetCompany(int company_id)
        {
            try
            {
                var company = await _companyService.GetCompany(company_id);
                if (company == null)
                    return NotFound();

                return Ok(new CompanyUserDTO(company));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // GET: api/v1/Companies/me
        [HttpGet("me")]
        public async Task<ActionResult<CompanyUserDTO>> GetCompanyMe()
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);
                return await GetCompany(int.Parse(tokenClaim.CompanyId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // PUT: api/v1/Companies/1
        [HttpPut("{company_id}")]
        public async Task<IActionResult> PutCompany(int company_id, [FromBody] CompanyDTO companyDTO)
        {
            try
            {
                if (company_id != companyDTO.Id)
                    return BadRequest();

                var company = await CompanyDAO.GetCompany(_context, company_id);
                if (company == null)
                    return NotFound("Company not found!");

                if (company.Name != companyDTO.Name)
                    return BadRequest("Impossible to change the name of the company!");

                if (companyDTO.MaxUsers <= 0)
                    return BadRequest("Not enough licences!");

                var users = await UserDAO.GetUsers(_context, company);
                if (users.Count > companyDTO.MaxUsers)
                    return BadRequest("Too many users in the base for the number of licenses");

                await _companyService.UpdateAsync(company, companyDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // PUT: api/v1/Companies/1
        [HttpPut("me")]
        public async Task<IActionResult> PutCompanyMe([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                var tokenClaim = await _authService.GetTokenClaimAsync(Request);
                return await PutCompany(int.Parse(tokenClaim.CompanyId), companyDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // POST: api/v1/Companies
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyNewDTO companyNewDTO)
        {
            try
            {
                if (companyNewDTO.MaxUsers <= 0)
                    return BadRequest("Not enough licences!");

                if (await CompanyDAO.ExistCompanyByName(_context, companyNewDTO.Name))
                    return BadRequest("Company already exist!");

                var company = await _companyService.RegisterAsync(companyNewDTO);

                return CreatedAtAction("GetCompany", new { company_id = company.Id }, new CompanyUserDTO(company));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }

        // DELETE: api/v1/Companies/1
        [HttpDelete("{company_id}")]
        [SwaggerResponse(204)]
        public async Task<IActionResult> DeleteCompany(int company_id)
        {
            try
            {
                var company = await CompanyDAO.GetCompany(_context, company_id);
                if (company == null)
                    return NotFound();

                await _companyService.RemoveCompany(company);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex));
            }
        }
    }
}
