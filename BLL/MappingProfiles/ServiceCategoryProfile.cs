using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.MappingProfiles
{
    internal class ServiceCategoryProfile : Profile
    {
        public ServiceCategoryProfile()
        {
            CreateMap<ServiceCategory, ServiceCategoryDTO>().ReverseMap();
        }
    }
}
