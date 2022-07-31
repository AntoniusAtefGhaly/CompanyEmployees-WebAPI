using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        RepositoryContext repositoryContext;
        ICompanyRepository  _companyRepository;
        IEmployeeRepository _employeeRepository;
        public ICompanyRepository Company {
            get
            {
                if (_companyRepository == null)
                {
                    _companyRepository = new CompanyRepository(repositoryContext);
                }
                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository== null)
                {
                    _companyRepository = new CompanyRepository(repositoryContext);
                }
                return _employeeRepository;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
