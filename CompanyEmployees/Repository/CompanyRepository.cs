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
    public class CompanyRepository: RepositoryBase<Company>,ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) :base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
              Create(company);
        }

        public void CreateCompanyCollection(IEnumerable<Company> Companies)
        {
            CreateCollection(Companies);
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
          return FindAll(trackChanges) 
                .OrderBy(c => c.Name)
                .ToList();
        }

        public IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> Ids, bool TrackChanging)
        {
            return FindAll(TrackChanging)
                .Where(c => Ids.Contains(c.Id));
        }

        public Company GetCompany(Guid CompanyId, bool TrackChanging)
        {
            return GetById(CompanyId, TrackChanging);
        }
    }
}
