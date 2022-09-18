using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) :base(repositoryContext)
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
                e => (e.Id == EmployeeID && e.CompanyId==CompanyId),trackChanges)
                .FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees(Guid CompanyId, bool trackChanges)
        {
            return FindByCondition(
                e => e.CompanyId == CompanyId, trackChanges)
                            .OrderBy(e => e.Name);
        }
    }
}