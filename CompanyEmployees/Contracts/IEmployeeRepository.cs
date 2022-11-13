using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        public PagedList<Employee> GetEmployees(Guid CompanyId, EmployeeParameters employeeParameters, bool trackChanges);
        public Employee GetEmployee(Guid CompanyId,Guid EmployeeID, bool trackChanges);
        public void CreateEmployee(Employee employee, Guid CompanyId);
        public void DeleteEmployee(Employee employee);
        public void UpdateEmployee(Employee employee, Guid companyId);
        public void CreateEmployeeCollection(IEnumerable<Employee> employees, Guid CompanyId);
    }
}
