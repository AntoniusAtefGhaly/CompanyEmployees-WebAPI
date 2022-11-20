using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/test/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CompaniesTestController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesTestController(IRepositoryManager repositorymanger,
            ILoggerManager loggerManager,
            IMapper mapper
            )
        {
            _repository = repositorymanger;
            _logger = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpPost]
        [HttpDelete]
        [HttpPut]
        [HttpPatch]
        [HttpHead]
        [HttpOptions]
        [RequireHttps]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyparamter)
        {
            var companies = await _repository.Company.GetAllCompaniesAsync(false, companyparamter);
            return Ok(companies);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpGet]
        [HttpPost]
        [HttpDelete]
        [HttpPut]
        [HttpPatch]
        [HttpHead]
        [HttpOptions]
        [RequireHttps]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyForCreationDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);

            _repository.Company.CreateCompany(company);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}