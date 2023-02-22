using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL;

namespace BLL.Services
{
    internal class ServiceCategoryServ : IServ<ServiceCategoryDTO>
    {
        private readonly IRepository<ServiceCategory> _serviceCategoryRepository;
        private readonly IMapper _mapper;

        public ServiceCategoryServ(IRepository<ServiceCategory> serviceCategoryRepository, IMapper mapper)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
            _mapper = mapper;
        }

        public void Add(ServiceCategoryDTO item)
        {
            var serviceCategory = _mapper.Map<ServiceCategoryDTO, ServiceCategory>(item);
            _serviceCategoryRepository.Create(serviceCategory);
        }

        public void DeleteById(int id)
        {
            var serviceCategory = _serviceCategoryRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (serviceCategory != null)
                _serviceCategoryRepository.Delete(serviceCategory);
        }

        public ServiceCategoryDTO FindById(int id)
        {
            var serviceCategory = _serviceCategoryRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var serviceCategoryDTO = _mapper.Map<ServiceCategory, ServiceCategoryDTO>(serviceCategory);
            return serviceCategoryDTO;
        }

        public List<ServiceCategoryDTO> GetAll() => _mapper.Map<List<ServiceCategory>, List<ServiceCategoryDTO>>(_serviceCategoryRepository.GetAll());

        public void Update(ServiceCategoryDTO item)
        {
            var serviceCategory = _mapper.Map<ServiceCategoryDTO, ServiceCategory>(item);
            _serviceCategoryRepository.Update(serviceCategory);
        }
    }
}
