using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            //_companyRepository = companyRepository;
            //_employeeRepository = employeeRepository;
        }

        public ICompanyRepository Company
        {
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
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(repositoryContext);
                }
                return _employeeRepository;
            }
        }

        public void Save()
        {
            repositoryContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await repositoryContext.SaveChangesAsync();
        }
    }
}