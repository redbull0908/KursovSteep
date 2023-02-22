using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.MappingProfiles
{
    internal class SubscribeProfile : Profile
    {
        public SubscribeProfile()
        {
            CreateMap<Subscribe, SubscribeDTO>().ReverseMap();
        }
    }
}
