using AutoMapper;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Company, CompanyDto>().
            //    ForMember(c => c.FullAddress,
            //    opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Company, CompanyDto>().
                ForMember(c => c.FullAddress, opt => opt.MapFrom(x => x.Country + "--" + x.Address));

            // CreateMap<Company, CompanyDto>()
            //.ForMember(c => c.FullAddress,opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<CompanyForCreationDto, Company>().ForMember(c => c.Id, opt => opt.MapFrom(x => System.Guid.NewGuid().ToString()));

            CreateMap<Employee, EmployeeDto>();
            //CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForCreationDto, Employee>().ForMember(e => e.Id, opt => opt.MapFrom(x => System.Guid.NewGuid().ToString()));
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}