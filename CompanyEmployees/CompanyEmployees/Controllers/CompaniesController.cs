﻿using AutoMapper;
using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CompanyEmployees.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositorymanger,
            ILoggerManager loggerManager,
            IMapper mapper
            )
        {
            _repository = repositorymanger;
            _logger = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            /*to test global error handler*/
            // throw new Exception("Exception");
            try
            {
                var companies = await _repository.Company.GetAllCompaniesAsync(false);
                _logger.LogInfo($"{nameof(GetCompanies)}==list of companies returned");
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
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex.Message}");
                return StatusCode(500, "internal server error " + ex.Message);
            }
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(id, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetCompany)} Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyForCreationDto companyDto)
        {
            if (companyDto == null)
            {
                _logger.LogError($"{nameof(CreateAsync)} CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto   is null");
            }
            var company = _mapper.Map<Company>(companyDto);

            _repository.Company.CreateCompany(company);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(company);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }

        [HttpGet("collection/{ids}", Name = "GetCompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError($"{nameof(GetCompanyCollection)}Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var companies =await _repository.Company.GetCompaniesByIdsAsync(ids, false);
            if (companies.Count() != ids.Count())
            {
                _logger.LogError($"{nameof(GetCompanyCollection)} some companies IDs not valid");
                return NotFound();
            }

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companiesDto);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companiesDto)
        {
            if (companiesDto == null)
            {
                _logger.LogError($"{nameof(CreateCompanyCollectionAsync)} user request have CompanyForCreationDto list of object is null");
                return BadRequest("CompanyForCreationDto list of object is null");
            }
            var companiesEntities = _mapper.Map<IEnumerable<Company>>(companiesDto);
            _repository.Company.CreateCompanyCollection(companiesEntities);
            await _repository.SaveAsync();
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesEntities);
            var ids = string.Join(',', companiesToReturn.Select(c => c.Id));
            return CreatedAtRoute("GetCompanyCollection", new { ids }, companiesToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var company = _repository.Company.GetCompany(id, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetCompany)} Company with id: {id} doesn't exist in the database.");
                return NotFound($"Company with id: { id} doesn't exist in the database.");
            }
            _repository.Company.Delete(company);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] CompanyForUpdateDto company, Guid id)
        {
            if (company == null)
            {
                _logger.LogError($"{nameof(UpdateCompanyAsync)}  the Company object is null from user request");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var CompanyEntity = _repository.Company.GetCompanyIncludeEmployees(id, true);
            if (CompanyEntity == null)
            {
                _logger.LogError($"{nameof(UpdateCompanyAsync)} Company with id = {id} doesnot exist in database");
                return NotFound($"Company with id = {id} doesnot exist in database");
            }

            _mapper.Map(company, CompanyEntity);
            //foreach (var e in CompanyEntity.Employees)
            //{
            //    e.CompanyId = id;
            //}
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
