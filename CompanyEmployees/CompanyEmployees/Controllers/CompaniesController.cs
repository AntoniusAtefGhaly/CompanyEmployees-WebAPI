using AutoMapper;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace CompanyEmployees.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositorymanger,
            ILoggerManager loggerManager,
            IMapper mapper
            )
        {
            _repository = repositorymanger;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            /*to test global error handler*/
            // throw new Exception("Exception");
            try
            {
                var companies = _repository.Company.FindAll(false);
                _loggerManager.LogInfo($"{nameof(GetCompanies)}==list of companies returned");
                //var companiesDto = companies.Select(c=>
                //    new 
                //    {
                //        Id=c.Id,
                //        Name=c.Name,
                //        FullAddress=c.Country+"-"+c.Address
                //    }
                //    );
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                //var company = companies.First();
                //var companydto = _mapper.Map<CompanyDto>(company);

                //var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);


                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex.Message}");
                return StatusCode(500, "internal server error " + ex.Message);
            }
        }
        
        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, false);
            if (company == null)
            {
                _loggerManager.LogError($"{nameof(GetCompany)} Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CompanyForCreationDto companyDto)
        {
            if (companyDto == null)
            {
                _loggerManager.LogError($"{nameof(Create)} CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto   is null");
            }
            var company = _mapper.Map<Company>(companyDto);

            _repository.Company.CreateCompany(company);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
        
        [HttpGet("collection/{ids}", Name = "GetCompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _loggerManager.LogError($"{nameof(GetCompanyCollection)}Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var companies = _repository.Company.GetCompaniesByIds(ids, false);
            if (companies.Count() != ids.Count())
            {
                _loggerManager.LogError($"{nameof(GetCompanyCollection)} some companies IDs not valid");
                return NotFound();
            }

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companiesDto);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companiesDto)
        {
            if (companiesDto == null)
            {
                _loggerManager.LogError($"{nameof(CreateCompanyCollection)} user request have CompanyForCreationDto list of object is null");
                return BadRequest("CompanyForCreationDto list of object is null");
            }
            var companiesEntities = _mapper.Map <IEnumerable<Company>>(companiesDto);
            _repository.Company.CreateCompanyCollection(companiesEntities);
            _repository.Save();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesEntities);
            var ids = string.Join(',', companiesToReturn.Select(c => c.Id));
            return CreatedAtRoute("GetCompanyCollection",new { ids }, companiesToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var company=_repository.Company.GetCompany(id,false);
            if (company == null)
            {
                _loggerManager.LogError($"{nameof(GetCompany)} Company with id: {id} doesn't exist in the database.");
                return NotFound($"Company with id: { id} doesn't exist in the database.");
            }
            _repository.Company.Delete(company);
            _repository.Save();
            return NoContent();
        }
    }
}
