using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class ScheduleServ : IServ<ScheduleDTO>
    {
        private IRepository<Schedule> _schedRepository;
        private readonly IMapper _mapper;

        public ScheduleServ(IRepository<Schedule> schedRepository, IMapper mapper)
        {
            _schedRepository = schedRepository;
            _mapper = mapper;
        }

        public void Add(ScheduleDTO item)
        {
            var sc = _mapper.Map<ScheduleDTO, Schedule>(item);
            _schedRepository.Create(sc);
        }

        public void DeleteById(int id)
        {
            var sc = _schedRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (sc != null)
                _schedRepository.Delete(sc);
        }

        public ScheduleDTO FindById(int id)
        {
            var sc = _schedRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var scDTO = _mapper.Map<Schedule, ScheduleDTO>(sc);
            return scDTO;
        }

        public List<ScheduleDTO> GetAll() => _mapper.Map<List<Schedule>, List<ScheduleDTO>>(_schedRepository.GetAll());

        public void Update(ScheduleDTO item)
        {
            var sc = _mapper.Map<ScheduleDTO, Schedule>(item);
            _schedRepository.Update(sc);
        }
    }
}
