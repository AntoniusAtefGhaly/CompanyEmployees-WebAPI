using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    //[Route("api/{v:apiversion}/companies")]  //for url versioning
    [Route("api/companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public CompaniesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyparamter)
        {
            var companies = await _repository.Company.GetAllCompaniesAsync(false, companyparamter);
            return Ok(companies);
        }
    }
}