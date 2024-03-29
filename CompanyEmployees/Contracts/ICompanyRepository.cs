﻿using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        IEnumerable<Company> GetAllCompanies(bool TrackChanging);

        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool TrackChanging, CompanyParameters companyparamter);

        Company GetCompany(Guid CompanyId, bool TrackChanging);

        Task<Company> GetCompanyAsync(Guid CompanyId, bool TrackChanging);

        public Company GetCompanyIncludeEmployees(Guid CompanyId, bool TrackChanging);

        void CreateCompany(Company company);

        void CreateCompanyCollection(IEnumerable<Company> Companies);

        IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> Ids, bool TrackChanging);

        Task<IEnumerable<Company>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool TrackChanging);

        void DeleteCompany(Company company);
    }
}