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
    [Route("api/Companies/{companyId}/Employees")]
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
        public IActionResult GetCompanyEmployees(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetCompanyEmployees)} company with id {companyId} not found ");
                return NotFound();
            }
            var employees = _repository.Employee.GetEmployees(companyId, false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }
        [HttpGet("{employeeId}", Name = "EmployeeById")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetEmployee)} company with id {companyId} not found");
                return NotFound();
            }
            var employee = _repository.Employee.GetEmployee(companyId, employeeId, true);
            if (employee == null)
            {
                _logger.LogError($"{nameof(GetEmployee)} employee with id {employeeId} not found");
                return NotFound();
            }
            var employyDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employyDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeForCreationDto employee, Guid companyId)
        {
            if (employee == null)
            {
                _logger.LogError($"{nameof(Create)}  the employee object is null from user request");
                return BadRequest("EmployeeForCreationDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(GetEmployee)} company with id {companyId} doesnot exsist in database ");
                return NotFound("company with id { companyId}    not found in database");
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repository.Employee.CreateEmployee(employeeEntity, companyId);
            _repository.Save();

            var returnEmployee = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("EmployeeById", new
            {
                companyId = employeeEntity.CompanyId,
                employeeId = employeeEntity.Id
            }, returnEmployee);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id,Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId,false);
            if(company == null)
            {
                _logger.LogError($"{nameof(Delete)} company with id = {id} doesnot exist in database");
                return NotFound($"company with id = {id} doesnot exist in database");
            }
            var employee = _repository.Employee.GetEmployee(companyId, id, false);
            if (employee == null)
            {
                _logger.LogError($"{nameof(Delete)} employee with id = {id} doesnot exist in database");
                return NotFound($"employee with id = {id} doesnot exist in database");
            }
            _repository.Employee.DeleteEmployee(employee);
            _repository.Save();
            return NoContent();

        }
        [HttpPost("collection")]
        public IActionResult CreateEmployeeCollection([FromBody] IEnumerable<EmployeeForCreationDto> employees, Guid companyId)
        {
            if (employees == null)
            {
                _logger.LogError($"{nameof(CreateEmployeeCollection)} user request have employees object is null");
                return BadRequest("employees object is null");
            }
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(CreateEmployeeCollection)} the company with id {companyId} doesnot exsist in database");
                return NotFound($"the company with id {companyId} doesnot exsist in database");
            }
            var employeesEntity = _mapper.Map<IEnumerable<Employee>>(employees);
            _repository.Employee.CreateEmployeeCollection(employeesEntity,companyId);
            _repository.Save();
            var employeesReturned = _mapper.Map<IEnumerable<EmployeeDto>>(employeesEntity);
            return Ok(employeesReturned);
            //return CreatedAtRoute("",);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromBody] EmployeeForUpdateDto employee,Guid companyId, Guid  id)
        {
            if (employee == null)
            {
                _logger.LogError($"{nameof(UpdateEmployee)}  the employee object is null from user request");
                return BadRequest("EmployeeForCreationDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, false);
            if (company == null)
            {
                _logger.LogError($"{nameof(UpdateEmployee)} the company with id {companyId} doesnot exsist in database");
                return NotFound($"the company with id {companyId} doesnot exsist in database");
            }
            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, true);
            if (employeeEntity == null)
            {
                _logger.LogError($"{nameof(UpdateEmployee)} employee with id = {id} doesnot exist in database");
                return NotFound($"employee with id = {id} doesnot exist in database");
            }
           _mapper.Map(employee, employeeEntity);
            _repository.Save();
            return Ok(employee);
        }
    }
}
