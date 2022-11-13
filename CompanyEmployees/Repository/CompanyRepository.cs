using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
          return FindAll(trackChanges) 
                .OrderBy(c => c.Name)
                .ToList();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges, CompanyParameters companyparamter)
        {
            return await FindAll(trackChanges)
                .Sort(companyparamter.OrderBy)
                .ToListAsync();
        }

        public IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> Ids, bool TrackChanging)
        {
            return FindAll(TrackChanging)
                .Where(c => Ids.Contains(c.Id));
        }

        public async Task<IEnumerable<Company>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool TrackChanging)
        {
            return await FindAll(TrackChanging)
            .Where(c => Ids.Contains(c.Id)).ToListAsync();
        }

        public Company GetCompany(Guid CompanyId, bool TrackChanging)
        {
            return GetById(CompanyId, TrackChanging);
        }

        public async Task<Company> GetCompanyAsync(Guid CompanyId, bool TrackChanging)
        {
            return await FindByCondition(c => c.Id.Equals(CompanyId), TrackChanging)
                .SingleOrDefaultAsync(); 
        }

        public Company GetCompanyIncludeEmployees(Guid CompanyId, bool TrackChanging)
        {
            return GetFirstInclude(e=>e.Id==CompanyId,c=>c.Employees, TrackChanging);
        }
    }
}
