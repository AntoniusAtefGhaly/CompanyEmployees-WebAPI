﻿using AutoMapper;
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
        private readonly IRepositoryManager _repositorymanger;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositorymanger,
            ILoggerManager loggerManager,
            IMapper mapper
            )
        {
            _repositorymanger = repositorymanger;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repositorymanger.Company.FindAll(false);
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
                _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex.Message}" );
                return StatusCode(404,"internal server error "+ex.Message);
            }
        }
    }
}
