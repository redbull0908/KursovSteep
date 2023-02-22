using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL;

namespace BLL.Services
{
    internal class ServiceServ : IServ<ServiceDTO>
    {
        private readonly IRepository<Service> _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceServ(IRepository<Service> serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public void Add(ServiceDTO item)
        {
            var doctor = _mapper.Map<ServiceDTO, Service>(item);
            _serviceRepository.Create(doctor);
        }

        public void DeleteById(int id)
        {
            var service = _serviceRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (service != null)
                _serviceRepository.Delete(service);
        }

        public ServiceDTO FindById(int id)
        {
            var service = _serviceRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var serviceDTO = _mapper.Map<Service, ServiceDTO>(service);
            return serviceDTO;
        }

        public List<ServiceDTO> GetAll() => _mapper.Map<List<Service>, List<ServiceDTO>>(_serviceRepository.GetAll());

        public void Update(ServiceDTO item)
        {
            var service = _mapper.Map<ServiceDTO, Service>(item);
            _serviceRepository.Update(service);
        }
    }
}
