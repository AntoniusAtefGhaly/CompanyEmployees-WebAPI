using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api/{companyId}/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public EmployeesController(IMapper mapper, IRepositoryManager repository, ILoggerManager logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public IActionResult GetCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetCompany)} company with id {companyId} not found ");
                return NotFound();
            }
            var employees = _repository.Employee.GetEmployees(companyId, false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var company = _repository.Company.GetCompany(companyId,false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetEmployee)} company with id {companyId} not found");
            return NotFound();
            }
            var employee=_repository.Employee.GetEmployee(companyId, employeeId,false);
            if(employee == null)
            {
                _logger.LogError($"{nameof(GetEmployee)} employee with id {employeeId} not found");
                return NotFound();
            }
            var employyDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employyDto);
        }
    }
}
