using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository: IRepositoryBase<Company>
    {
        IEnumerable<Company> GetAllCompanies(bool TrackChanging);
        Company GetCompany(Guid CompanyId,bool TrackChanging);
        void CreateCompany(Company company);
        void CreateCompanyCollection(IEnumerable<Company> Companies);
        IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> Ids,bool TrackChanging); 
        void DeleteCompany (Company company);
        
    }
}
