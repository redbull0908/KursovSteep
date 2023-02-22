using AutoMapper;
using BLL.DTO;
using DAL;

namespace BLL.MappingProfiles
{
    internal class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
        }
    }
}
