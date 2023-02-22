using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.MappingProfiles
{
    internal class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>().ReverseMap();
        }
    }
}
