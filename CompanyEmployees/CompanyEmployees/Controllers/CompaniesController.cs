using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CompanyEmployees.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repositorymanger;
        private readonly ILoggerManager _loggerManager;

        public CompaniesController(IRepositoryManager repositorymanger, ILoggerManager loggerManager)
        {
            _repositorymanger = repositorymanger;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var companies = _repositorymanger.Company.FindAll(false);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
