using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        public IEnumerable<Employee> GetEmployees(Guid CompanyId, bool trackChanges);
        public Employee GetEmployee(Guid CompanyId,Guid EmployeeID, bool trackChanges);
    }
}
