using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL;

namespace BLL.Services
{
    internal class DoctorServ : IServ<DoctorDTO>
    {
        private IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorServ(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public void Add(DoctorDTO item)
        {
           var doctor = _mapper.Map<DoctorDTO,Doctor>(item);
           _doctorRepository.Create(doctor);
        }

        public void DeleteById(int id)
        {
           var doctor = _doctorRepository.GetAll().FirstOrDefault(x=>x.Id == id);

           if(doctor != null)
                _doctorRepository.Delete(doctor);
        }

        public DoctorDTO FindById(int id)
        {
            var doctor = _doctorRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var doctorDTO = _mapper.Map<Doctor,DoctorDTO>(doctor);
            return doctorDTO;
        }

        public List<DoctorDTO> GetAll() => _mapper.Map<List<Doctor>, List<DoctorDTO>>(_doctorRepository.GetAll());

        public void Update(DoctorDTO item)
        {
            var doctor = _mapper.Map<DoctorDTO, Doctor>(item);
            _doctorRepository.Update(doctor);
        }
    }
}
