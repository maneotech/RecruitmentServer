using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentServer.Models;
using RecruitmentServer.Models.Enums;
using RecruitmentServer.Services;
using RecruitmentServer.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecruitmentServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILoggerService _loggerService;

        public CompaniesController(ICompanyService companyService, ILoggerService loggerService)
        {
            this.companyService = companyService;
            _loggerService = loggerService;
        }


        // GET: api/<CompaniesController>
        [HttpGet]
        public ActionResult<List<Company>> Get()
        {
            _loggerService.LogInformation(LoggerInformationEnum.COMPANY_GETALL);
            return companyService.GetCompanies();
        }

        // GET api/<CompaniesController>/5
        [HttpGet("{id}")]
        public ActionResult<Company> Get(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.COMPANY_GET);

            var company = companyService.GetById(id);

            if (company == null)
            {
                return NotFound($"Company with Id = {id} not found");
            }

            return company;
        }

        // POST api/<CompaniesController>
        [HttpPost]
        public ActionResult Post([FromBody] Company company)
        {
            _loggerService.LogInformation(LoggerInformationEnum.COMPANY_CREATE);

            if (companyService.GetByName(company.Name) == null)
            {
                companyService.Create(company);
                return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<CompaniesController>/5
        [HttpPut]
        public ActionResult Put([FromBody] Company companyToUpdate)
        {
            _loggerService.LogInformation(LoggerInformationEnum.COMPANY_UPDATE);

            string? companyId = companyToUpdate.Id;
            if (companyId == null)
            {
                return BadRequest();
            }

            var company = companyService.GetById(companyId);

            if (company == null)
            {
                return NotFound($"Company with Id = {companyId} not found");
            }

            companyService.Update(companyToUpdate);

            return NoContent();

        }

        [HttpDelete]
        public ActionResult Delete([FromBody] string companyId)
        {
            _loggerService.LogInformation(LoggerInformationEnum.COMPANY_DELETE);
            companyService.DeleteById(companyId);
            return Ok();

        }


    }
}
