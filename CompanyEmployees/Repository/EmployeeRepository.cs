using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEmployee(Employee employee, Guid CompanyId)
        {
            employee.CompanyId = CompanyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public Employee GetEmployee(Guid CompanyId, Guid EmployeeID, bool trackChanges)
        {
            return FindByCondition(
                e => (e.Id == EmployeeID && e.CompanyId == CompanyId), trackChanges)
                .FirstOrDefault();
        }

        public PagedList<Employee> GetEmployees(Guid CompanyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var entities = FindByCondition(
                e => (
                e.CompanyId == CompanyId)
                , trackChanges).
                FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge).
                Search(employeeParameters.SearchTerm).
                            Sort(employeeParameters.OrderBy).
                            Skip(employeeParameters.PageSize * (employeeParameters.PageNumber - 1)).
                            Take(employeeParameters.PageSize);

            var count = FindByCondition(
                 e => (
                e.CompanyId == CompanyId)
                , trackChanges).
                FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge).
                Search(employeeParameters.SearchTerm).Count();

            return new PagedList<Employee>(entities.ToList(), count, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public void CreateEmployeeCollection(IEnumerable<Employee> employees, Guid CompanyId)
        {
            foreach (var emp in employees)
            {
                emp.CompanyId = CompanyId;
            }
            CreateCollection(employees);
        }

        public void UpdateEmployee(Employee employee, Guid companyId)
        {
            employee.CompanyId = companyId;
            Update(employee);
        }
    }
}