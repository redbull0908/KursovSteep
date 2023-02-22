using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL;

namespace BLL.Services
{
    internal class SubscribeServ : IServ<SubscribeDTO>
    {
        private IRepository<Subscribe> _subscribeRepository;
        private readonly IMapper _mapper;

        public SubscribeServ(IRepository<Subscribe> subscribeRepository, IMapper mapper)
        {
            _subscribeRepository = subscribeRepository;
            _mapper = mapper;
        }

        public void Add(SubscribeDTO item)
        {
            var sub = _mapper.Map<SubscribeDTO, Subscribe>(item);
            _subscribeRepository.Create(sub);
        }

        public void DeleteById(int id)
        {
            var sub = _subscribeRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (sub != null)
                _subscribeRepository.Delete(sub);
        }

        public SubscribeDTO FindById(int id)
        {
            var sub = _subscribeRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var subDTO = _mapper.Map<Subscribe, SubscribeDTO>(sub);
            return subDTO;
        }

        public List<SubscribeDTO> GetAll() => _mapper.Map<List<Subscribe>, List<SubscribeDTO>>(_subscribeRepository.GetAll());

        public void Update(SubscribeDTO item)
        {
            var sub = _mapper.Map<SubscribeDTO, Subscribe>(item);
            _subscribeRepository.Update(sub);
        }
    }
}
