using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.MappingProfiles
{
    internal class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDTO>().ReverseMap();
        }
    }
}
